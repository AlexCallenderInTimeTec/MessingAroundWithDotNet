using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MessingAroundWithDotNet.Data;
using MessingAroundWithDotNet.DataTransferObjects.Character;
using MessingAroundWithDotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace MessingAroundWithDotNet.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
            
        }

        public async Task<ServiceResponse<List<GetCharacterDataTransferObjects>>> AddCharacter(AddCharacterDataTransferObjects newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDataTransferObjects>>();
            var character = _mapper.Map<Character>(newCharacter);
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            serviceResponse.Data = 
               await _context.Characters.Select(c => _mapper.Map<GetCharacterDataTransferObjects>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDataTransferObjects>>> DeleteCharacter(int id)
        {
             var serviceResponse = new ServiceResponse<List<GetCharacterDataTransferObjects>>();

            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
                if (character == null)
                    throw new Exception($"Character with Id '{id}' not found.");

                _context.Remove(character);

                await _context.SaveChangesAsync();

                serviceResponse.Data = 
                    await _context.Characters.Select(c => _mapper.Map<GetCharacterDataTransferObjects>(c)).ToListAsync();

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
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDataTransferObjects>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDataTransferObjects>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDataTransferObjects>();
            var dbCharacters = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDataTransferObjects>(dbCharacters);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDataTransferObjects>> UpdateCharacter(UpdateCharacterDataTransferObjects updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDataTransferObjects>();

            try
            {
                var character = 
                    await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
                if (character == null)
                    throw new Exception($"Charter with Id '{updatedCharacter.Id}' not found.");

                character.Name = updatedCharacter.Name;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Strength = updatedCharacter.Strength;
                character.Defense = updatedCharacter.Defense;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Class = updatedCharacter.Class;

                await _context.SaveChangesAsync();
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