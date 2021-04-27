using BlazorRPG.Shared;
using BlazorRPG.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorRPG.Server.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<bool> UserExists(string username);
           
    }
}
