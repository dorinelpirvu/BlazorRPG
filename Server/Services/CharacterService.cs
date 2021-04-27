using AutoMapper;
using BlazorRPG.Shared.Dtos;
using BlazorRPG.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorRPG.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.Extensions.Hosting;

namespace BlazorRPG.Server.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IRpgClassService _rpgclassService;
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        private readonly IHostEnvironment _environment;

        public CharacterService(IMapper mapper,IRpgClassService rpgClassService, DataContext dataContext,IHostEnvironment environment)
        {
            _rpgclassService = rpgClassService;
            _mapper = mapper;
            _dataContext = dataContext;
            _environment = environment;
        }

        public async Task<ServiceResponse<AddCharacterDto>> CreateCharacter(AddCharacterDto newCharacter)
        {
            ServiceResponse<AddCharacterDto> serviceresponse = new ServiceResponse<AddCharacterDto>();
            Character character = _mapper.Map<Character>(newCharacter);
            await _dataContext.Characters.AddAsync(character);
            await _dataContext.SaveChangesAsync();
            serviceresponse.Data = newCharacter;
            //serviceresponse.Data = (_dataContext.Characters.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
            serviceresponse.Success = true;
            return serviceresponse;
        }

        public async  Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> serviceresponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character character = await _dataContext.Characters.FirstOrDefaultAsync(x => x.Id == id);
                _dataContext.Characters.Remove(character);
               await  _dataContext.SaveChangesAsync();
                serviceresponse.Data= (_dataContext.Characters.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
            }
            catch (Exception ex)
            {

                serviceresponse.Success = false;
                serviceresponse.Message = ex.Message;
            }
            return serviceresponse;
        }

        public async Task<List<Character>> GetCharacterByClassUrl(string url)
        {
            List<Character> characters = new List<Character>();
            RpgClass rpg = await _rpgclassService.GetRpgClassbyUrl(url);
            return await _dataContext.Characters.Include(q=>q.Class).Where(x => x.RpgClassId == rpg.Id).ToListAsync();
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterbyId(int id)
        {

            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            Character dbcharacter = await _dataContext.Characters.Include(q=>q.Class).FirstOrDefaultAsync(x => x.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbcharacter);
            serviceResponse.Success = true;
            if (VerificareExistentaFile(dbcharacter.DocumentPDF))
            {
                serviceResponse.Message = dbcharacter.DocumentPDF;
            }
            else
            {
                serviceResponse.Message = string.Empty;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> ListaCaractere()
        {
            ServiceResponse<List<GetCharacterDto>> serviceresponse = new ServiceResponse<List<GetCharacterDto>>();
            List<Character> dbCharacters = await _dataContext.Characters.ToListAsync();
            serviceresponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            serviceresponse.Success = true;
            return serviceresponse;

        }


        public async Task<ServiceResponse<Character>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
            ServiceResponse<Character> serviceResponse = new ServiceResponse<Character>();
            try
            {

                Character dbcharacter = await _dataContext.Characters.FirstOrDefaultAsync(x => x.Id == updateCharacter.Id);
                dbcharacter.Name = updateCharacter.Name;
                dbcharacter.Strength = updateCharacter.Strength;
                dbcharacter.Points = updateCharacter.Points;
                dbcharacter.Inteligence = updateCharacter.Inteligence;
                dbcharacter.RpgClassId = updateCharacter.RpgClassId;
                dbcharacter.DocumentPDF = updateCharacter.DocumentPDF;
                _dataContext.Characters.Update(dbcharacter);
                await _dataContext.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<Character>(dbcharacter);
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        private bool  VerificareExistentaFile(string numePDF  )
        {

            var cale = Path.Combine(_environment.ContentRootPath, "Pdfuri", numePDF);
            if (System.IO.File.Exists(cale))
            {
             return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}