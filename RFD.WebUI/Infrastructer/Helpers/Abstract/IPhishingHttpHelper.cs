using RFD.Entities.Common.Model;
using RFD.Entities.DTO;
using RFD.WebUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RFD.Entities.Common.FilterModel;

namespace RFD.WebUI.Infrastructer.Helpers
{
    public interface IPhishingHttpHelper
    {
        Task<ApiResponse<PhishingModel>> AddAsync(Phishing phishing);
        Task<ApiResponse<Phishing>> UpdateAsync(Phishing phishing);
        Task<ApiResponse<Phishing>> DeleteAsync(int id);
        Task<ApiResponse<PagingResponse<PhishingModel>>> GetAllAsync(PhishingParamerters phishing);
        Task<ApiResponse<bool>> StartCampaign(PhishingModel phishing);
        Task<ApiResponse<bool>> PauseCampaign(int id);
        Task<ApiResponse<Phishing>> Duplicate(Phishing phishing);
        Task<ApiResponse<bool>> SelectedDelete(List<Phishing> phishings);
    }
}
