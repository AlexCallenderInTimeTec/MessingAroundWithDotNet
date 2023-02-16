using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessingAroundWithDotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace MessingAroundWithDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
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

        [HttpGet("GetAll")]
        public ActionResult<List<Character>> Get()
        {
            return Ok(Characters);
        }

        [HttpGet("{id}")]
        public ActionResult<Character> GetSingle(int id)
        {
            return Ok(Characters.FirstOrDefault(x => x.Id == id));
        }
    
    }

}