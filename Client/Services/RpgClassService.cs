using Blazored.LocalStorage;
using BlazorRPG.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorRPG.Client.Services
{
    public class RpgClassService : IRpgClassService
    {
        public List<RpgClass> RpgClasses { get; set; } = new List<RpgClass>();
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;
        public RpgClassService(HttpClient http,ILocalStorageService localStorage)
        {
            _http = http;
            _localStorage = localStorage;
        }
        public async Task LoadRpgClasses()
        {
            //pentru ca se incarca la prima rulare,posibil fara logare,si pt ca controllerul este cu authentificare este 
            //nevoie daca returneaza mesaj 401 
            var request = new HttpRequestMessage(HttpMethod.Get, "api/RpgClass");
             HttpResponseMessage response = await _http.SendAsync(request);
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                RpgClasses= JsonConvert.DeserializeObject<List<RpgClass>>(content);
                
               //var rpg= await _http.GetFromJsonAsync<List<RpgClass>>("api/RpgClass");
              
            }
            else
            {
                RpgClasses = new List<RpgClass>();
            }
            

        }
        //pt a obtine token si al trmite in header 
        private async Task<string> GetBearerToken()
        {
            return await _localStorage.GetItemAsync<string>("authToken");


        }
    }
}
