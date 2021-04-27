using BlazorRPG.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorRPG.Server.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RpgClassController:ControllerBase
    {
        private readonly IRpgClassService _rpgClassService;
        public RpgClassController(IRpgClassService  rpgClassService)
        {
            _rpgClassService = rpgClassService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            return Ok(await _rpgClassService.ListaRpgClass()); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _rpgClassService.GetRpgClassbyId(id));
        }


    }
}
