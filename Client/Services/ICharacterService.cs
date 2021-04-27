using BlazorRPG.Shared;
using BlazorRPG.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorRPG.Client.Services
{
    public interface ICharacterService
    {
        event Action OnChange;
        List<Character> Characters { get; set; }
        Task LoadCharacters(string categoryUrl = null);
        Task<ServiceResponse<Character>> GetCharacter(int id);
        Task<ServiceResponse<AddCharacterDto>> Create(AddCharacterDto addCharacterDto);
        Task<ServiceResponse<Character>> Update(Character updatecharacter);
        //Task<string> LoadFile(string numefisier = null);
        Task<string> LoadFileX(string numefisier = null);
    }
}
