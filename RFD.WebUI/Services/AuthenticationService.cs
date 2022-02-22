using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RFD.Entities.DTO;
using RFD.Entities.VM;
using RFD.WebUI.Infrastructer.Auths;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RFD.WebUI.Services
{
    public interface IAuthenticationService
    {
        User User { get; }
        Task Initialize();
        Task Login(LoginVM login);
        Task Logout();
    }

    public class AuthenticationService : IAuthenticationService
    {
        private HttpClient Http;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _customAuthenticationProvider;
        public User User { get; set; }
        IHttpService _httpService;

        public AuthenticationService(
            HttpClient httpClient,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService,
            AuthenticationStateProvider customAuthenticationProvider,
            IHttpService httpService
            )
        {
            Http = httpClient;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
            _customAuthenticationProvider = customAuthenticationProvider;
            _httpService = httpService;
        }

        public async Task Initialize()
        {
            User = await _localStorageService.GetItem<User>("user");
        }

        public async Task Login(LoginVM login)
        {
            var response = await _httpService.Post<UserVM>("api/auth/login", login);


            await _localStorageService.SetItem("user", response);
            (_customAuthenticationProvider as CustomAuthStateProvider).Notify();
            _navigationManager.NavigateTo("/");


        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItem("user");
            (_customAuthenticationProvider as CustomAuthStateProvider).Notify();
            _navigationManager.NavigateTo("/auth");
        }
    }
}
