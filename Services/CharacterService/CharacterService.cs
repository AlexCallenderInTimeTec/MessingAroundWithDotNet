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
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character
            {
                Id = 1,
                Name = "Alex"
            }
        };
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDataTransferObjects>>> AddCharacter(AddCharacterDataTransferObjects newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDataTransferObjects>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;
            characters.Add(character);
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDataTransferObjects>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDataTransferObjects>>> DeleteCharacter(int id)
        {
             var serviceResponse = new ServiceResponse<List<GetCharacterDataTransferObjects>>();

            try
            {
                var character = characters.FirstOrDefault(c => c.Id == id);
                if (character == null)
                    throw new Exception($"Character with Id '{id}' not found.");

                characters.Remove(character);

                serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDataTransferObjects>(c)).ToList();

            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }
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

        public async Task<ServiceResponse<GetCharacterDataTransferObjects>> UpdateCharacter(UpdateCharacterDataTransferObjects updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDataTransferObjects>();

            try
            {
                var character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);
                if (character == null)
                    throw new Exception($"Charter with Id '{updatedCharacter.Id}' not found.");

                // _mapper.Map(updatedCharacter, character);    

                character.Name = updatedCharacter.Name;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Strength = updatedCharacter.Strength;
                character.Defense = updatedCharacter.Defense;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Class = updatedCharacter.Class;

                serviceResponse.Data = _mapper.Map<GetCharacterDataTransferObjects>(character);

            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }
            return serviceResponse;
        }
    }
}