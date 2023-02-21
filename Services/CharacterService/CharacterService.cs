using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<ServiceResponse<List<GetCharacterDataTransferObjects>>> AddCharacter(AddCharacterDataTransferObjects newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDataTransferObjects>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.User = await _context.Users.FirstOrDefaultAsync(U => U.Id == GetUserId());

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            serviceResponse.Data = 
               await _context.Characters
                    .Where(c => c.User!.Id == GetUserId())
                    .Select(c => _mapper.Map<GetCharacterDataTransferObjects>(c))
                    .ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDataTransferObjects>>> DeleteCharacter(int id)
        {
             var serviceResponse = new ServiceResponse<List<GetCharacterDataTransferObjects>>();

            try
            {
                var character = await _context.Characters
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId());
                if (character == null || character.User!.Id != GetUserId())
                    throw new Exception($"Character with Id '{id}' not found.");

                _context.Remove(character);

                await _context.SaveChangesAsync();

                serviceResponse.Data = 
                    await _context.Characters
                        .Where(c => c.User!.Id == GetUserId())
                        .Select(c => _mapper.Map<GetCharacterDataTransferObjects>(c))
                        .ToListAsync();
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
            var dbCharacters = await _context.Characters.Where(c => c.User!.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDataTransferObjects>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDataTransferObjects>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDataTransferObjects>();
            var dbCharacters = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id && x.User!.Id == GetUserId());
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