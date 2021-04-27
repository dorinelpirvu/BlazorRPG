using BlazorRPG.Server.Services;
using BlazorRPG.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorRPG.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
   [ApiController]
    public class CharacterController:ControllerBase
    {
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            
            return Ok(await _characterService.ListaCaractere());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacterbyId(id));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(AddCharacterDto newCharacter)
        {
           var response =await _characterService.CreateCharacter(newCharacter);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("update")]

        public async Task<IActionResult>Update(UpdateCharacterDto updateCharacter)
        {
            return Ok(await _characterService.UpdateCharacter(updateCharacter));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _characterService.DeleteCharacter(id));
        }
        [HttpGet("RpgClass/{url}")]
        public async Task<IActionResult> GetCharactersbyClassUrl(string url)
        {

            return Ok(await _characterService.GetCharacterByClassUrl(url));
        }
    }
}
