using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessingAroundWithDotNet.Models;

namespace MessingAroundWithDotNet.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private List<Character> Characters = new List<Character>
        {
            new Character(),
            new Character
            {
                Id = 1,
                Name = "Alex"
            }
        };

        public async Task<List<Character>> AddCharacter(Character newCharacter)
        {
            Characters.Add(newCharacter);
            return Characters;
        }

        public async Task<List<Character>> GetAllCharacters()
        {
            return Characters;
        }

        public async Task<Character> GetCharacterById(int id)
        {
            var character = Characters.FirstOrDefault(x => x.Id == id);
            if (character != null)
                return character;  
            throw new Exception("Character Not Found"); 
        }
    }
}