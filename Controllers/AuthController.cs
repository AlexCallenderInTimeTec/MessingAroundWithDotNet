using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessingAroundWithDotNet.Data;
using MessingAroundWithDotNet.DataTransferObjects.User;
using MessingAroundWithDotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace MessingAroundWithDotNet.Controllers
{
    [ApiController]
    [Route("controller")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDataTransferObjects request)
        {
            var response = await _authRepository.Register(
                new User { Username = request.Username }, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(UserLoginDataTransferObjects request)
        {
            var response = await _authRepository.Login(request.Username, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}