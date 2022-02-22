using RFD.Entities.Common.FilterModel;
using RFD.Entities.Common.Model;
using RFD.Entities.DTO;
using RFD.WebUI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RFD.WebUI.Infrastructer.Helpers
{
    public interface IInsiderHttpHelper
    {
        Task<ApiResponse<InsiderModel>> AddAsync(Insider insider);
        Task<ApiResponse<Insider>> UpdateAsync(Insider insider);
        Task<ApiResponse<Insider>> DeleteAsync(int id);
        Task<ApiResponse<PagingResponse<InsiderModel>>> GetAllAsync(InsiderParamerters paramerters);
        Task<ApiResponse<bool>> StartCampaign(InsiderModel insider);
        Task<ApiResponse<bool>> PauseCampaign(int id);
        Task<ApiResponse<Insider>> Duplicate(Insider insider);
        Task<ApiResponse<bool>> SelectedDelete(List<Insider> insiders);
    }
}
