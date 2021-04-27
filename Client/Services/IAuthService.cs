using BlazorRPG.Shared;
using BlazorRPG.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorRPG.Client.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserDto userrequest);

        Task<ServiceResponse<string>> Login(UserLogin userrequest);
    }
}
