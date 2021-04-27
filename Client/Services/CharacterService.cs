using BlazorRPG.Shared;
using BlazorRPG.Shared.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorRPG.Client.Services
{
    public class CharacterService : ICharacterService
    {
        public List<Character> Characters { get; set; } = new List<Character>();
        public event Action OnChange;
        private readonly HttpClient _http;
        
       
        public CharacterService(HttpClient http)
        {
            _http = http;
        }
       
                
        public async Task<ServiceResponse<Character>> GetCharacter(int id)
        {
           return  await _http.GetFromJsonAsync<ServiceResponse<Character>>($"api/Character/{id}");                      
        }

        public async Task LoadCharacters(string categoryUrl = null)
        {
            if (categoryUrl == null)
            {
                Characters = await _http.GetFromJsonAsync<List<Character>>("api/Character");
            }
            else
            {
                Characters = await _http.GetFromJsonAsync<List<Character>>($"api/Character/RpgClass/{categoryUrl}");
            }
            OnChange.Invoke();
        }

        public async  Task<ServiceResponse<AddCharacterDto>> Create(AddCharacterDto addCharacterDto)
        {
            var result = await _http.PostAsJsonAsync("api/Character/Create", addCharacterDto);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<AddCharacterDto>>();
        }


        public async Task<string> LoadFileX(string numefisier = null)
        {
            var httpResponseMessage = await _http.GetAsync($"api/Images/DownloadServerFile/{numefisier}");
              
            return httpResponseMessage.Content.ReadAsStringAsync().Result;          
        }

        public async Task<ServiceResponse<Character>> Update(Character updateCharacter)
        {
            var result = await _http.PutAsJsonAsync("api/Character/Update", updateCharacter);

            return await result.Content.ReadFromJsonAsync<ServiceResponse<Character>>();
        }
    }
    
}
