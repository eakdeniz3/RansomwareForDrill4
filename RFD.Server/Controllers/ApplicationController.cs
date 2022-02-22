using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RFD.Bussiness.EntityFramework.Services.Abstact;
using RFD.Entities.Common.Model;
using RFD.Entities.DTO;
using RFD.Entities.Enum;
using RFD.Server.Infrastructer.Extensions;
using RFD.Server.Infrastructer.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RFD.Server.Controller
{






    [Route("api/[Controller]")]
    [ApiController]
   // [ApiKeyAttribute]
    public class ApplicationController : ControllerBase
    {
        IUnitOfWork _unitOfWork;
        IRFDStarterExtension _rFDStarterExtension;
        public static volatile Dictionary<int, bool> _pauseDict = new Dictionary<int, bool>();
        readonly IHubContext<RFDHub> _hubContext;
        public ApplicationController(IRFDStarterExtension rFDStarterExtension, IUnitOfWork unitOfWork = null, IHubContext<RFDHub> hubContext = null)
        {
            _rFDStarterExtension = rFDStarterExtension;
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }

      

        [HttpGet("{id}/did-true-closed")]
        public async Task<ApiResponse<bool>> DidTrueClosed(int id)
        {
            try
            {

                var computersRunningApp = await _unitOfWork.SummaryService.FindAsync(id);

                if (computersRunningApp is not null)
                {
                    computersRunningApp.DidTrueClose = true;
                }
                var result = await _unitOfWork.Complete();

                if (result > default(int))
                {
                    return ApiResponse<bool>.Success(true);
                }
                else
                {
                    return ApiResponse<bool>.Fail("Beklenmedik bir sorun ile karşılaşıldı.");
                }
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ApiResponse<int>> AddSummaryAsync(Summary summary)
        {
            if (!ModelState.IsValid)
            {
                List<string> errors = new List<string>();
                foreach (var item in ModelState.Values.SelectMany(x => x.Errors))
                    errors.Add(item.ErrorMessage);
                return ApiResponse<int>.Fail(errors.ToArray());
            }

            var model = await _unitOfWork.SummaryService.AddAsync(new Summary()
            {
                ApplicationType = summary.ApplicationType,
                ComputerName = summary.ComputerName,
                UpdatedDate = DateTime.Now,
                UserName = summary.UserName

            });
            var result = await _unitOfWork.Complete();
            if (result > default(int))
            {
                // await _hubContext.Clients.All.SendAsync("ReceiveInsider", "transection", template);
                return ApiResponse<int>.Success(model.Id);
            }
            else
            {
                return ApiResponse<int>.Fail("Veri kaydedilemedi. Lütfen verilerinizi kontrol ediniz.");
            }



            //try
            //{
            //    var insiderModel = await _unitOfWork.SummaryService.FindAsync(s);
            //    if (insiderModel.Status != StatusType.InProgress)
            //        return ApiResponse<bool>.Fail("Görev başlatılımış durumda değil.");

            //    if (_pauseDict.TryGetValue(id, out bool isStop))
            //        _pauseDict[id] = true;
            //    insiderModel.Status = StatusType.Pause;
            //    var result = await _unitOfWork.Complete();
            //    //_stopDict.Remove(id);
            //    if (result > default(int))
            //        return ApiResponse<bool>.Success(true);
            //    else
            //        return ApiResponse<bool>.Fail("Beklenmedik bir sorun ile karşılaşıldı.");
            //}
            //catch (Exception ex)
            //{
            //    return ApiResponse<bool>.Fail(ex.Message);
            //}
        }

        //[HttpGet("get-summary-count")]
        //public async Task<ApiResponse<int>> GetSummaryCountAsync()
        //{

        //    var count = await _unitOfWork.SummaryService.Quereble().CountAsync();
        //    return ApiResponse<int>.Success(count);

        //}


    }
}
