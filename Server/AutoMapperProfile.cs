using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorRPG.Shared.Dtos;
using BlazorRPG.Shared;

namespace BlazorRPG.Server
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto,Character>();
            //CreateMap<UpdateCharacterDto, Character>();
            CreateMap<User, UserDto>();
        }
    }
}
