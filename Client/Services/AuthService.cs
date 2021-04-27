using BlazorRPG.Shared;
using BlazorRPG.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorRPG.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<string>> Login(UserLogin userlogin)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Auth/login", userlogin);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<int>> Register(UserDto userrequest)
        {
            var result= await _httpClient.PostAsJsonAsync("api/Auth/register", userrequest);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }
    }
}
