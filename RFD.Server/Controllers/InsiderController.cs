using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RFD.Bussiness.EntityFramework.Services.Abstact;
using RFD.Entities.Common.FilterModel;
using RFD.Entities.Common.Model;
using RFD.Entities.DTO;
using RFD.Entities.Enum;
using RFD.Server.Infrastructer.Attributes;
using RFD.Server.Infrastructer.Extensions;
using RFD.Server.Infrastructer.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RFD.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiKeyAttribute]
    public class InsiderController : ControllerBase
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IRFDStarterExtension _starterExtension;
        readonly IHubContext<RFDHub> _hubContext;
        public static volatile Dictionary<int, bool> _pauseDict = new Dictionary<int, bool>();

        public InsiderController(IUnitOfWork unitOfWork, IHubContext<RFDHub> hubContext, IRFDStarterExtension starterExtension)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
            _starterExtension = starterExtension;
        }

        [HttpPost]
        public async Task<ApiResponse<Insider>> Post([FromBody] Insider template)
        {
            if (ModelState.IsValid)
            {
                //template.Transections = new List<Transection>();
                //foreach (var computerName in template.Computers.Split(',').Where(x => x != ""))
                //{
                //    template.Transections.Add(new Transection
                //    {
                //        ComputerName = computerName,
                //        TransectionType = TransectionType.None,
                //        ApplicationType = ApplicationType.Insider

                //    });
                //}
                var model = await _unitOfWork.InsiderService.AddAsync(template);
                var result = await _unitOfWork.Complete();
                if (result > default(int))
                {
                    // await _hubContext.Clients.All.SendAsync("ReceiveInsider", "transection", template);
                    return ApiResponse<Insider>.Success(model);
                }
                else
                {
                    return ApiResponse<Insider>.Fail("Veri kaydedilemedi. Lütfen verilerinizi kontrol ediniz.");
                }
            }
            else
            {
                List<string> errors = new List<string>();
                foreach (var item in ModelState.Values.SelectMany(x => x.Errors))
                    errors.Add(item.ErrorMessage);
                return ApiResponse<Insider>.Fail(errors.ToArray());
            }

        }
        [HttpGet]
        public async Task<ApiResponse<List<Insider>>> GetAsync([FromQuery] InsiderParamerters paramerters)
        {

            var insiders = await _unitOfWork.InsiderService.Get(paramerters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(insiders.MetaData));
            if (insiders is null)
            {
                return ApiResponse<List<Insider>>.Fail("Kayıtlı veri bulunamadı.");
            }
            return ApiResponse<List<Insider>>.Success(insiders);
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<Insider>> GetAsync(int id)
        {
            var template = await _unitOfWork.InsiderService.GetByIdAsync(id);
            if (template is null)
            {
                return ApiResponse<Insider>.Fail("Kayıtlı veri bulunamadı.");
            }
            return ApiResponse<Insider>.Success(template);
        }

        [HttpPost("start-campaign")]
        public async Task<ApiResponse<bool>> StartCampaign(Insider insider)
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
                                var isStart = _starterExtension.Start(item.ComputerName, ApplicationType.Insider);
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

        [HttpGet("pause-campaign/{id}")]
        public async Task<ApiResponse<bool>> CampaignPauseAsync(int id)
        {
            try
            {
                var insiderModel = await _unitOfWork.InsiderService.FindAsync(id);
                if (insiderModel.Status != StatusType.InProgress)
                    return ApiResponse<bool>.Fail("Görev başlatılımış durumda değil.");

                if (_pauseDict.TryGetValue(id, out bool isStop))
                    _pauseDict[id] = true;
                insiderModel.Status = StatusType.Pause;
                var result = await _unitOfWork.Complete();
                //_stopDict.Remove(id);
                if (result > default(int))
                    return ApiResponse<bool>.Success(true);
                else
                    return ApiResponse<bool>.Fail("Beklenmedik bir sorun ile karşılaşıldı.");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail(ex.Message);
            }
        }


        [HttpPost("duplicate")]
        public async Task<ApiResponse<Insider>> Duplicate(Insider insider)
        {
            if (!ModelState.IsValid)
            {
                List<string> errors = new List<string>();
                foreach (var item in ModelState.Values.SelectMany(x => x.Errors))
                    errors.Add(item.ErrorMessage);

                return ApiResponse<Insider>.Fail(errors.ToArray());
            }
            var entity = await _unitOfWork.InsiderService.FindAsync(insider.Id);

            if (entity is null)
            {
                return ApiResponse<Insider>.Fail("Kayıt kopyalanamadı. Kayda ilişkin örnek bulunamadı. ");
            }

            var newEntity = new Insider
            {
                Title = insider.Title + "-Copy",
                Computers = insider.Computers,

            };

            var duplicate = await _unitOfWork.InsiderService.AddAsync(newEntity);
            var result = await _unitOfWork.Complete();

            if (result > default(int))
            {
                return ApiResponse<Insider>.Success(duplicate);
            }
            else
            {
                return ApiResponse<Insider>.Fail("Kopyalama işlemi başarısız.");
            }

        }
        [HttpPost("selected-delete")]
        public async Task<ApiResponse<bool>> SelectedDeleteAsync(List<Insider> insiders)
        {

            List<Insider> deletedList = new List<Insider>();
            List<string> errors = new List<string>();
            foreach (var item in insiders)
            {
                var entity = await _unitOfWork.InsiderService.FindAsync(item.Id);
                if (entity is null)
                {
                    errors.Add($"{entity.Id} ID numaralı kayıt bulunamadı.");
                }
                else if (entity.Status == StatusType.InProgress)
                {
                    errors.Add($"{entity.Title} isimli işlem devam etmekte.");
                }
                else
                {
                    deletedList.Add(entity);
                }
            }

            await _unitOfWork.InsiderService.RemoveRangeAsync(deletedList);
            var result = await _unitOfWork.Complete();
            if (result > default(int))
            {
                return ApiResponse<bool>.Success(true);
            }
            else
            {
                return ApiResponse<bool>.Fail(errors.ToArray());
            }

        }

        [HttpPut("{id}")]
        public async Task<ApiResponse<Insider>> PutAsync(int id, [FromBody] Insider data)
        {
            if (!ModelState.IsValid)
            {
                List<string> errors = new List<string>();
                foreach (var item in ModelState.Values.SelectMany(x => x.Errors))
                    errors.Add(item.ErrorMessage);

                return ApiResponse<Insider>.Fail(errors.ToArray());
            }
            var entity = await _unitOfWork.InsiderService.FindAsync(id);

            if (entity is null)
            {
                return ApiResponse<Insider>.Fail("Kayıtlı veri bulunamadı.");
            }

            if (entity.Status != StatusType.None)
            {
                return ApiResponse<Insider>.Fail("İşlem tamamlandığı için güncelleme yapılamamaktadır.");
            }



            entity.Title = data.Title;
            entity.Computers = data.Computers;
            //  data.Id = entity.Id;

            await _unitOfWork.InsiderService.UpdateAsync(entity);
            var result = await _unitOfWork.Complete();
            if (result > default(int))
            {
                return ApiResponse<Insider>.Success(data);
            }
            else
            {
                return ApiResponse<Insider>.Fail("Veri kaydedilemedi. Lütfen verilerinizi kontrol ediniz.");
            }
        }


        [HttpDelete("{id}")]
        public virtual async Task<ApiResponse<Insider>> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.InsiderService.FindAsync(id);

            if (entity is null)
            {
                return ApiResponse<Insider>.Fail("Kayıtlı veri bulunamadı.");
            }

            if (entity.Status == StatusType.InProgress)
            {
                return ApiResponse<Insider>.Fail("İşlem devam etmekte. Silmek için durdurunuz veya bitmesini bekleyiniz.");
            }

            await _unitOfWork.InsiderService.RemoveAsync(entity);
            var result = await _unitOfWork.Complete();
            if (result > default(int))
            {
                return ApiResponse<Insider>.Success(entity);
            }
            else
            {
                return ApiResponse<Insider>.Fail("Veri silinemedi. ");
            }

        }

    }
}
