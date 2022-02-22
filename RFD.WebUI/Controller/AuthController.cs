using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RFD.Bussiness.EntityFramework.Services.Abstact;
using RFD.Entities.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RFD.WebUI.Controller
{
    [Route("[controller]")]
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
