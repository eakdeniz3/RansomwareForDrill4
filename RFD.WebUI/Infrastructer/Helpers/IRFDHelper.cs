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
    public interface IRFDHelper
    {
        Task<ApiResponse<bool>> StartInsiderCampaign(InsiderModel insider);
        Task<ApiResponse<bool>> PauseInsiderCampaign(int id);
      
    }
}
