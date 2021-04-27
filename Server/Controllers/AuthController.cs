using BlazorRPG.Server.Services;
using BlazorRPG.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorRPG.Shared.Dtos;

namespace BlazorRPG.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

       [HttpPost("register")]
        public async Task <IActionResult> Register(UserDto userrequest)
        {
            
                var response = await _authService.Register(
                    new User { Name = userrequest.Name }, userrequest.Password);
                if (!response.Success)
                {
                return BadRequest(response);
                }

               return Ok(response);
            
        }
        [HttpPost("login")]
        public async Task<IActionResult>Login(UserLogin userrequest)
        {

            var response = await _authService.Login(userrequest.Name,userrequest.Password);
                if (!response.Success)
                {
                    return BadRequest(response);
                }
            
            return Ok(response);

        }
    }
}
