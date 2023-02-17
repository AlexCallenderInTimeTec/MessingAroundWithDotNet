using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MessingAroundWithDotNet.DataTransferObjects.Character;
using MessingAroundWithDotNet.Models;

namespace MessingAroundWithDotNet.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private List<Character> characters = new List<Character>
        {
            new Character(),
            new Character
            {
                Id = 1,
                Name = "Alex"
            }
        };

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDataTransferObjects>>> AddCharacter(AddCharacterDataTransferObjects newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDataTransferObjects>>();
            characters.Add(_mapper.Map<Character>(newCharacter));
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDataTransferObjects>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDataTransferObjects>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDataTransferObjects>>();
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDataTransferObjects>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDataTransferObjects>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDataTransferObjects>();
            var character = characters.FirstOrDefault(x => x.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDataTransferObjects>(character);
            return serviceResponse;
        }
    }
}