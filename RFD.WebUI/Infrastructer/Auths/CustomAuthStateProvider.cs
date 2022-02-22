using Microsoft.AspNetCore.Components.Authorization;
using RFD.Entities.DTO;
using RFD.Entities.VM;
using RFD.WebUI.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace RFD.WebUI.Infrastructer.Auths
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        //public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        //{
        //    string token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiVG9ueSBTdGFyayIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6Iklyb24gTWFuIiwiZXhwIjozMTY4NTQwMDAwfQ.IbVQa1lNYYOzwso69xYfsMOHnQfO3VLvVqV2SOXS7sTtyyZ8DEf5jmmwz2FGLJJvZnQKZuieHnmHkg7CGkDbvA";

        //    var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
        //    //var identity = new ClaimsIdentity();

        //    var user = new ClaimsPrincipal(identity);
        //    var state = new AuthenticationState(user);

        //    NotifyAuthenticationStateChanged(Task.FromResult(state));

        //    return state;
        //}

        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }

    //internal void Notify()
    //{
    //    throw new NotImplementedException();
    //}

    private readonly ILocalStorageService _localStorageService;

        public CustomAuthStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var user = await _localStorageService.GetItem<UserVM>("user");
                if (user == null)
                {
                    var anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity() { }));
                    return anonymous;
                }
                var stream = user.Token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = jsonToken as JwtSecurityToken;

                var userClaimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(user.Token), "JWTAUTH"));
                var loginUser = new AuthenticationState(userClaimPrincipal);
                return loginUser;
            }
            catch (Exception)
            {
                var anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity() { }));
                return anonymous;

            }
        }

        public void Notify()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

    }
}