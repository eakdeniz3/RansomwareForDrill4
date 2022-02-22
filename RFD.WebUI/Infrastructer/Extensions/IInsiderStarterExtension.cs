using RFD.Entities.Common.Model;
using RFD.Entities.DTO;
using RFD.Entities.Enum;
using System.Threading.Tasks;

namespace RFD.WebUI.Infrastructer.Extensions
{
    public interface IInsiderStarterExtension
    {
        Task<ApiResponse<bool>> Start(Insider insider);
    }
}