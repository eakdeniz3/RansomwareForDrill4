using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RFD.Bussiness.EntityFramework.Services.Abstact;
using RFD.Entities.Common.Model;
using RFD.Entities.DTO;
using RFD.Entities.Enum;
using RFD.WebUI.Infrastructer.Hubs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RFD.WebUI.Infrastructer.Extensions
{
    public class InsiderStarterExtension : IInsiderStarterExtension
    {
        IUnitOfWork _unitOfWork;
        IRFDStarterExtension _rFDStarterExtension;
        public static volatile Dictionary<int, bool> _pauseDict = new Dictionary<int, bool>();
        readonly IHubContext<RFDHub> _hubContext;
        public InsiderStarterExtension(IRFDStarterExtension rFDStarterExtension, IUnitOfWork unitOfWork = null, IHubContext<RFDHub> hubContext = null)
        {
            _rFDStarterExtension = rFDStarterExtension;
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }

        public async Task<ApiResponse<bool>> Start(Insider insider)
        {
            try
            {
                if (insider is null)
                    return ApiResponse<bool>.Fail("Kampanya seçilmedi.");

                List<string> errors = new List<string>();
                List<Transection> transections = new List<Transection>();
                var insiderModel = await _unitOfWork.InsiderService.Quereble().Include(x => x.Transections).FirstOrDefaultAsync(x => x.Id == insider.Id);
                if (insiderModel is not null && (insiderModel.Status == StatusType.None || insiderModel.Status == StatusType.Pause))
                {
                    if (!_pauseDict.TryGetValue(insider.Id, out bool isStop))
                        _pauseDict.Add(insider.Id, false);
                    else
                        _pauseDict[insider.Id] = false;

                    if (!insiderModel.Transections.Any())
                    {
                        foreach (var computerName in insiderModel.Computers.Split(',').Where(x => x != ""))
                        {
                            transections.Add(new Transection
                            {
                                InsiderId = insiderModel.Id,
                                ComputerName = computerName,
                                TransectionType = TransectionType.Progress,
                            });
                        }
                        await _unitOfWork.TransectionService.AddRangeAsync(transections);
                        insiderModel.Status = StatusType.InProgress;
                        var result = await _unitOfWork.Complete();

                        if (result > default(int))
                        {
                            insiderModel.Transections = transections;
                        }
                        else
                        {
                            errors.Add($"{insider.Title} görevi başlatılırken bir sorun ile karşılaşıldı.");
                        }
                    }
                    await Task.Run(async () =>
                    {
                        foreach (var item in insiderModel?.Transections.Where(x => x.TransectionType != TransectionType.Success).OrderBy(x => x.Id))
                        {
                            _pauseDict.TryGetValue(insider.Id, out bool isStop);

                            if (!isStop)
                            {
                                // await Task.Delay(200);
                                insiderModel.Status = StatusType.InProgress;
                                var isStart = _rFDStarterExtension.Start(item.ComputerName, item.Id);
                                if (isStart)
                                {
                                    item.TransectionType = TransectionType.Success;
                                }
                                else
                                {
                                    item.TransectionType = TransectionType.UnSuccess;
                                }
                                if (insiderModel.Transections.Count == insiderModel.Transections.Count(x => x.TransectionType == TransectionType.Success))
                                    insiderModel.Status = StatusType.Complate;
                            }
                            else
                            {
                                insiderModel.Status = StatusType.Pause;
                                // item.TransectionType = TransectionType.Stop;
                            }
                            int result = await _unitOfWork.Complete();
                            string json = JsonConvert.SerializeObject(insiderModel, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                            await _hubContext.Clients.All.SendAsync("ReceiveInsider", "transection", json);
                        }
                    });
                }
                else
                {
                    errors.Add($"{insider.Title} görevi {insider.Status} durumunda.");
                }
                if (errors.Any())
                {
                    return ApiResponse<bool>.Fail(errors.ToArray());
                }
                else
                {
                    return ApiResponse<bool>.Success(true);
                }
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail(ex.Message);
            }
        }
    }
}
