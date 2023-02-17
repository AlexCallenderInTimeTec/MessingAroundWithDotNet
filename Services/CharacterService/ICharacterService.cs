using MessingAroundWithDotNet.DataTransferObjects.Character;
using MessingAroundWithDotNet.Models;

namespace MessingAroundWithDotNet.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDataTransferObjects>>> GetAllCharacters();
        Task<ServiceResponse<GetCharacterDataTransferObjects>> GetCharacterById(int id);
        Task<ServiceResponse<List<GetCharacterDataTransferObjects>>> AddCharacter(AddCharacterDataTransferObjects newCharacter);
        Task<ServiceResponse<GetCharacterDataTransferObjects>> UpdateCharacter(UpdateCharacterDataTransferObjects updatedCharacter);
        Task<ServiceResponse<List<GetCharacterDataTransferObjects>>> DeleteCharacter(int id);
    }
}