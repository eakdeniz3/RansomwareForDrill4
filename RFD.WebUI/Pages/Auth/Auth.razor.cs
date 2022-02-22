using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RFD.Entities.VM;
using RFD.WebUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace RFD.WebUI.Pages.Auth
{
    public partial class Auth
    {

        LoginVM login = new LoginVM();

        public bool IsLoginPage { get; set; } = true;

        [CascadingParameter]
        private Task<AuthenticationState> authenticationState { get; set; }
        [Inject]
        IAuthenticationService authenticationService { get; set; }

        protected override async Task OnInitializedAsync()
        {
             var auth =await authenticationState;
            if (auth.User.Identity.IsAuthenticated)
            {
                navigationManager.NavigateTo("/");
            } 
        }

        public void SignIn()
        {
            IsLoginPage = true;
            StateHasChanged();
        }


        public void SignUp()
        {
            IsLoginPage = false;
            StateHasChanged();
        }

        public async Task Login()
        {
            await authenticationService.Login(login);
        }


       

    }
}
