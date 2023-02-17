using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessingAroundWithDotNet.DataTransferObjects.Character;
using MessingAroundWithDotNet.Models;
using MessingAroundWithDotNet.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace MessingAroundWithDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {

        private readonly ICharacterService _characterService;


        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDataTransferObjects>>>> Get()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDataTransferObjects>>> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterDataTransferObjects>>> AddCharacter(AddCharacterDataTransferObjects newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDataTransferObjects>>>> UpdateCharacter(UpdateCharacterDataTransferObjects updatedCharacter)
        {
            var respone = await _characterService.UpdateCharacter(updatedCharacter);
            if (respone.Data == null){
                return NotFound(respone);
            }

            return Ok(respone);
        }
    }
}