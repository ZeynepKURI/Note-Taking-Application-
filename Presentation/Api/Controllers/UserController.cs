
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        private readonly IAuthService authService;

        public UserController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> LoginUserIn(LoginDTO loginDTO)
        {
            var result = await authService.LoginAsync(loginDTO);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<LoginResponse>> RegisterUserDTO(RegisterDTO registerDTO)

        {
            var result = await authService.RegisterAsync(registerDTO);

            return Ok(result);
        }

        }
}