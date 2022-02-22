
using RFD.Entities.DTO;
using RFD.Entities.VM;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RFD.Bussiness.EntityFramework.Services.Abstact
{

    public interface IUserService:IRepository<User>
    {
        Task<User> FindByUserNameUserAsync(string userName);
      
        Task<bool> CheckPasswordAsync(User user, string providedPassword);
        Task<UserVM> GenerateJwtTokenAsync(User user);
        Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
        Task CreateUserAsync(User user,string password);
    }
}
