using RFD.Entities.Common.FilterModel;
using RFD.Entities.Common.Model;
using RFD.Entities.DTO;
using RFD.WebUI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RFD.WebUI.Infrastructer.Helpers
{
    public interface ISummaryHttpHelper
    {

        Task<ApiResponse<Summary>> DeleteAsync(int id);
        Task<ApiResponse<PagingResponse<SummaryModel>>> GetAllAsync(SummaryParamerters paramerters);
        Task<ApiResponse<bool>> SelectedDelete(List<Summary> transections);

        Task<ApiResponse<CountData>> GetCountDataAsync();
    }
}
