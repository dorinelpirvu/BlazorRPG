
using BlazorRPG.Shared;
using BlazorRPG.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorRPG.Server.Services
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> ListaCaractere();
        Task<ServiceResponse<GetCharacterDto>> GetCharacterbyId(int id);
        Task<ServiceResponse<AddCharacterDto>> CreateCharacter(AddCharacterDto newCharacter);
        Task<ServiceResponse<Character>> UpdateCharacter(UpdateCharacterDto updateCharacter);
        Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
        Task<List<Character>> GetCharacterByClassUrl(string url);
    }
}