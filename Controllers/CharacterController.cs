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
        private static Character Zombie = new Character();

        [HttpGet]
        public ActionResult<Character> Get()
        {
            return Ok(Zombie);
        }
    
    }

}