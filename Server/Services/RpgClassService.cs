using BlazorRPG.Server.Data;
using BlazorRPG.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorRPG.Server.Services
{
    public class RpgClassService : IRpgClassService
    {
        
        private readonly DataContext _dataContext;
        public RpgClassService(DataContext dataContext)
        {
            
            _dataContext = dataContext;
        }
        
        
        public async Task<ServiceResponse<RpgClass>> GetRpgClassbyId(int id)
        {
            ServiceResponse<RpgClass> serviceResponse = new ServiceResponse<RpgClass>();
            RpgClass rpg = await _dataContext.RpgClasses.FirstOrDefaultAsync(x => x.Id == id);
            serviceResponse.Data = rpg;
            serviceResponse.Success = true;
            return serviceResponse;
        }

       
        public async Task<List<RpgClass>> ListaRpgClass()
        {
            ServiceResponse<List<RpgClass>> serviceresponse = new ServiceResponse<List<RpgClass>>();
            List<RpgClass> rpg = await _dataContext.RpgClasses.ToListAsync();
            return rpg.ToList();
        }

        public async  Task<RpgClass> GetRpgClassbyUrl(string url)
        {
           RpgClass rpg= await _dataContext.RpgClasses.FirstOrDefaultAsync(c => c.Url.ToLower().Equals(url.ToLower()));
            return rpg;
        }
    }
}
