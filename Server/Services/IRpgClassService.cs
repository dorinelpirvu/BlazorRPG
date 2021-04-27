using BlazorRPG.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorRPG.Server.Services
{
    public interface IRpgClassService
    {
        Task<List<RpgClass>> ListaRpgClass();
        Task<ServiceResponse<RpgClass>> GetRpgClassbyId(int id);
        Task<RpgClass> GetRpgClassbyUrl(string url);
       
        
    }
}
