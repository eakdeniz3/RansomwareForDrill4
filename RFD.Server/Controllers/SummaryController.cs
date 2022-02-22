using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RFD.Bussiness.EntityFramework.Services.Abstact;
using RFD.Entities.Common.FilterModel;
using RFD.Entities.Common.Model;
using RFD.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RFD.Server.Controllers
{
    [Route("api/summary")]
    [ApiController]
    public class SummaryController : ControllerBase
    {
        public readonly IUnitOfWork _unitOfWork;
        public SummaryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public async Task<ApiResponse<List<Summary>>> GetAsync([FromQuery] SummaryParamerters paramerters)
        {
            var models = await _unitOfWork.SummaryService.Get(paramerters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(models.MetaData));
            if (models is null)
            {
                return ApiResponse<List<Summary>>.Fail("Kayıtlı veri bulunamadı.");
            }
            return ApiResponse<List<Summary>>.Success(models);
        }

        [HttpGet("get-count-data")]
        public async Task<ApiResponse<CountData>> GetCountDataAsync()
        {

            var count = await _unitOfWork.SummaryService.GetCountDataAsync();
            if (count is null)
            {
                return ApiResponse<CountData>.Fail("Kayıtlı veri bulunamadı.");
            }
            return ApiResponse<CountData>.Success(count);
        }




        [HttpPost("update")]
        public async Task<ApiResponse<bool>> UpdateAsync(Summary computersRunningApp)
        {
            try
            {

                var model = await _unitOfWork.SummaryService.FindAsync(computersRunningApp.Id);

                if (model is not null)
                {
                    model.ApplicationType = computersRunningApp.ApplicationType;
                    model.UpdatedDate = DateTime.Now;
                    model.UserName = computersRunningApp.UserName;
                    model.ComputerName = model.ComputerName is not null ? model.ComputerName : computersRunningApp.ComputerName;
                    model.DidItWork = computersRunningApp.DidItWork;

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
        public async Task<ApiResponse<int>> PostAsync(Summary computersRunningApp)
        {
            if (!ModelState.IsValid)
            {
                List<string> errors = new List<string>();
                foreach (var item in ModelState.Values.SelectMany(x => x.Errors))
                    errors.Add(item.ErrorMessage);
                return ApiResponse<int>.Fail(errors.ToArray());
            }

            try
            {

                var entity = await _unitOfWork.SummaryService.AddAsync(computersRunningApp);
                var result = await _unitOfWork.Complete();

                if (result > default(int))
                {
                    return ApiResponse<int>.Success(entity.Id);
                }
                else
                {
                    return ApiResponse<int>.Fail("Beklenmedik bir sorun ile karşılaşıldı.");
                }
            }
            catch (Exception ex)
            {
                return ApiResponse<int>.Fail(ex.Message);
            }
        }



        [HttpPost("selected-delete")]
        public async Task<ApiResponse<bool>> SelectedDeleteAsync(List<Summary> computersRunningApps)
        {

            List<Summary> deletedList = new List<Summary>();
            List<string> errors = new List<string>();
            foreach (var item in computersRunningApps)
            {
                var entity = await _unitOfWork.SummaryService.FindAsync(item.Id);
                if (entity is null)
                {
                    errors.Add($"{entity.Id} ID numaralı kayıt bulunamadı.");
                }

                else
                {
                    deletedList.Add(entity);
                }
            }

            await _unitOfWork.SummaryService.RemoveRangeAsync(deletedList);
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

        [HttpDelete("{id}")]
        public virtual async Task<ApiResponse<Summary>> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.SummaryService.FindAsync(id);

            if (entity is null)
            {
                return ApiResponse<Summary>.Fail("Kayıtlı veri bulunamadı.");
            }



            await _unitOfWork.SummaryService.RemoveAsync(entity);
            var result = await _unitOfWork.Complete();
            if (result > default(int))
            {
                return ApiResponse<Summary>.Success(entity);
            }
            else
            {
                return ApiResponse<Summary>.Fail("Veri silinemedi. ");
            }

        }



    }
}
