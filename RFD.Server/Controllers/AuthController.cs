using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RFD.Bussiness.EntityFramework.Services.Abstact;
using RFD.Entities.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RFD.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IUnitOfWork _unitOfWork;
        public AuthController(
              IUnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost("login")]
        public async Task<UserVM> LoginAsync([FromBody] LoginVM loginVM)
        {

            if (!ModelState.IsValid)
                return null;

            var user = await _unitOfWork.UserService.FindByUserNameUserAsync(loginVM.Username);
            if (user != null)
            {
                var result = await _unitOfWork.UserService.CheckPasswordAsync(user, loginVM.Password);
                if (result)
                {
                    var userVM = await _unitOfWork.UserService.GenerateJwtTokenAsync(user);
                    return userVM;

                }
            }
            return null;
        }
    }
}
