using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RFD.Bussiness.EntityFramework.Services.Abstact;
using RFD.Entities.Common.FilterModel;
using RFD.Entities.Common.Model;
using RFD.Entities.DTO;
using RFD.Entities.Enum;
using RFD.Server.Infrastructer.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RFD.Server.Controllers
{
    [Route("api/phishing")]
    [ApiController]
    public class PhishingController : ControllerBase
    {
        public readonly IUnitOfWork _unitOfWork;
        readonly IHubContext<RFDHub> _hubContext;
        public static volatile Dictionary<int, bool> _stopDict = new Dictionary<int, bool>();

        public PhishingController(IUnitOfWork unitOfWork, IHubContext<RFDHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }

        //    [HttpPost]
        //    public async Task<ApiResponse<Phishing>> Post([FromBody] Phishing template)
        //    {
        //        if (ModelState.IsValid)
        //        {

        //            var model = await _unitOfWork.TableService.AddAsync(template);
        //            var result = await _unitOfWork.Complete();
        //            if (result > default(int))
        //            {
        //               // await _hubContext.Clients.All.SendAsync("ReceivePhishing", "transection", template);
        //                return ApiResponse<Phishing>.Success(model);
        //            }
        //            else
        //            {
        //                return ApiResponse<Phishing>.Fail("Veri kaydedilemedi. Lütfen verilerinizi kontrol ediniz.");
        //            }
        //        }
        //        else
        //        {
        //            List<string> errors = new List<string>();
        //            foreach (var item in ModelState.Values.SelectMany(x => x.Errors))
        //                errors.Add(item.ErrorMessage);
        //            return ApiResponse<Phishing>.Fail(errors.ToArray());
        //        }

        //    }
        //    [HttpGet]
        //    public async Task<ApiResponse<List<Phishing>>> GetAsync([FromQuery] PhishingParamerters phishingParamerters)
        //    {

        //        var phishings = await _unitOfWork.PhishingService.Get(phishingParamerters);
        //        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(phishings.MetaData));
        //        if (phishings is  null)
        //        {
        //            return ApiResponse<List<Phishing>>.Fail("Kayıtlı veri bulunamadı.");
        //        }
        //        return ApiResponse<List<Phishing>>.Success(phishings);
        //    }

        //    [HttpGet("{id}")]
        //    public async Task<ApiResponse<Phishing>> GetAsync(int id)
        //    {
        //        var template = await _unitOfWork.TableService.GetByIdAsync(id);
        //        if (template is null)
        //        {
        //            return ApiResponse<Phishing>.Fail("Kayıtlı veri bulunamadı.");
        //        }
        //        return ApiResponse<Phishing>.Success(template);
        //    }

        //    [HttpPost("start-campaign")]
        //    public async Task<ApiResponse<bool>> StartCampaign(Phishing phishing)
        //    {
        //        try
        //        {
        //            if (phishing is null)
        //                return ApiResponse<bool>.Fail("Kampanya seçilmedi.");

        //            List<string> errors = new List<string>();
        //            List<Transection> transections = new List<Transection>();
        //            var phishingModel = await _unitOfWork.PhishingService.Quereble().Include(x => x.Transections).FirstOrDefaultAsync(x => x.Id == phishing.Id);
        //            if (phishingModel is not null && (phishingModel.Status == StatusType.None || phishingModel.Status == StatusType.Pause))
        //            {
        //                if (!_stopDict.TryGetValue(phishing.Id, out bool isStop))
        //                    _stopDict.Add(phishing.Id, false);
        //                else
        //                    _stopDict[phishing.Id] = false;

        //                if (!phishingModel.Transections.Any())
        //                {

        //                    await _unitOfWork.TransectionService.AddRangeAsync(transections);
        //                    phishingModel.Status = StatusType.InProgress;
        //                    var result = await _unitOfWork.Complete();

        //                    if (result > default(int))
        //                    {
        //                        phishingModel.Transections = transections;
        //                    }
        //                    else
        //                    {
        //                        errors.Add($"{phishing.Title} görevi başlatılırken bir sorun ile karşılaşıldı.");
        //                    }
        //                }
        //                await Task.Run(async () =>
        //                    {
        //                        foreach (var item in phishingModel?.Transections.Where(x => x.TransectionType != TransectionType.Success).OrderBy(x => x.Id))
        //                        {
        //                            _stopDict.TryGetValue(phishing.Id, out bool isStop);

        //                            if (!isStop)
        //                            {
        //                                await Task.Delay(200);
        //                                phishingModel.Status = StatusType.InProgress;
        //                                item.TransectionType = TransectionType.Success;
        //                                if (phishingModel.Transections.Count == phishingModel.Transections.Count(x => x.TransectionType == TransectionType.Success))
        //                                    phishingModel.Status = StatusType.Complate;
        //                            }
        //                            else
        //                            {
        //                                phishingModel.Status = StatusType.Pause;
        //                                // item.TransectionType = TransectionType.Stop;
        //                            }
        //                            int result = await _unitOfWork.Complete();
        //                            string json = JsonConvert.SerializeObject(phishingModel, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        //                            await _hubContext.Clients.All.SendAsync("ReceivePhishing", "transection", json);
        //                        }
        //                    });
        //            }
        //            else
        //            {
        //                errors.Add($"{phishing.Title} görevi {phishing.Status} durumunda.");
        //            }
        //            if (errors.Any())
        //            {
        //                return ApiResponse<bool>.Fail(errors.ToArray());
        //            }
        //            else
        //            {
        //                return ApiResponse<bool>.Success(true);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return ApiResponse<bool>.Fail(ex.Message);
        //        }
        //    }

        //    [HttpGet("pause-campaign/{id}")]
        //    public async Task<ApiResponse<bool>> CampaignPauseAsync(int id)
        //    {
        //        try
        //        {
        //            var phishingModel = await _unitOfWork.PhishingService.FindAsync(id);
        //            if (phishingModel.Status != StatusType.InProgress)
        //                return ApiResponse<bool>.Fail("Görev başlatılımış durumda değil.");

        //            if (_stopDict.TryGetValue(id, out bool isStop))
        //                _stopDict[id] = true;
        //            phishingModel.Status = StatusType.Pause;
        //            var result = await _unitOfWork.Complete();
        //            //_stopDict.Remove(id);
        //            if (result > default(int))
        //                return ApiResponse<bool>.Success(true);
        //            else
        //                return ApiResponse<bool>.Fail("Beklenmedik bir sorun ile karşılaşıldı.");
        //        }
        //        catch (Exception ex)
        //        {
        //            return ApiResponse<bool>.Fail(ex.Message);
        //        }
        //    }



        //    [HttpPost("selected-delete")]
        //    public async Task<ApiResponse<bool>> SelectedDeleteAsync(List<Phishing> phishings)
        //    {

        //        List<Phishing> deletedList = new List<Phishing>();
        //        List<string> errors = new List<string>();
        //        foreach (var item in phishings)
        //        {
        //            var entity = await _unitOfWork.PhishingService.FindAsync(item.Id);
        //            if (entity is null)
        //            {
        //                errors.Add($"{entity.Id} ID numaralı kayıt bulunamadı.");
        //            }
        //            else if (entity.Status == StatusType.InProgress)
        //            {
        //                errors.Add($"{entity.Title} isimli işlem devam etmekte.");
        //            }
        //            else
        //            {
        //                deletedList.Add(entity);
        //            }
        //        }

        //        await _unitOfWork.PhishingService.RemoveRangeAsync(deletedList);
        //        var result = await _unitOfWork.Complete();
        //        if (result > default(int))
        //        {
        //            return ApiResponse<bool>.Success(true);
        //        }
        //        else
        //        {
        //            return ApiResponse<bool>.Fail(errors.ToArray());
        //        }

        //    }


        //    public Task SendMail(string emails, string body)
        //    {
        //        MailMessage message = new MailMessage();
        //        message.From = new MailAddress("personel@hvkk.tsk", "Personel Başkanlığı");

        //        message.Subject = "Tatbikat";
        //        message.IsBodyHtml = true;
        //        try
        //        {
        //            using (SmtpClient smtp = new SmtpClient("128.20.21.12", 25))
        //            {
        //                message.To.Clear();


        //                message.To.Add(new MailAddress(emails));

        //                message.Body = body;
        //                message.IsBodyHtml = true;
        //                smtp.EnableSsl = false;
        //                smtp.UseDefaultCredentials = true;
        //                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //                smtp.Send(message);
        //            }
        //        }
        //        catch (Exception)
        //        {
        //        }

        //        return Task.CompletedTask;
        //    }

        //    [HttpDelete("{id}")]
        //    public virtual async Task<ApiResponse<Phishing>> DeleteAsync(int id)
        //    {
        //        var entity = await _unitOfWork.PhishingService.FindAsync(id);

        //        if (entity is null)
        //        {
        //            return ApiResponse<Phishing>.Fail("Kayıtlı veri bulunamadı.");
        //        }

        //        if (entity.Status == StatusType.InProgress)
        //        {
        //            return ApiResponse<Phishing>.Fail("İşlem devam etmekte. Silmek için durdurunuz veya bitmesini bekleyiniz.");
        //        }

        //        await _unitOfWork.TableService.RemoveAsync(entity);
        //        var result = await _unitOfWork.Complete();
        //        if (result > default(int))
        //        {
        //            return ApiResponse<Phishing>.Success(entity);
        //        }
        //        else
        //        {
        //            return ApiResponse<Phishing>.Fail("Veri silinemedi. ");
        //        }

        //    }

        //}
    }
}