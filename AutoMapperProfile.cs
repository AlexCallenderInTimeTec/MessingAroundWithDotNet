using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MessingAroundWithDotNet.DataTransferObjects.Character;
using MessingAroundWithDotNet.Models;

namespace MessingAroundWithDotNet
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDataTransferObjects>();
            CreateMap<AddCharacterDataTransferObjects, Character>();
            CreateMap<UpdateCharacterDataTransferObjects, Character>();
        }
    }
}