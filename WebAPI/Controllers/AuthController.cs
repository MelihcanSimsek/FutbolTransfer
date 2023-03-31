using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExist = _authService.UserExists(userForRegisterDto.Email);
            if(userExist.Success)
            {
                return BadRequest(userExist);
            }
            var registerResult = _authService.Register(userForRegisterDto);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if(result.Success)
            {
               return Ok(result);
            }
           return BadRequest(result);
        }


        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userCheck = _authService.Login(userForLoginDto);
            if(!userCheck.Success)
            {
                return BadRequest(userCheck.Message);
            }
            var result = _authService.CreateAccessToken(userCheck.Data);
            if(result.Success)
            {
                return Ok(result);
            }
               return BadRequest(result);
        }
    }
}
