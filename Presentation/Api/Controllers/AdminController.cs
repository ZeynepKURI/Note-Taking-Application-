
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAuthService authService;
        public AdminController(IAuthService _authService)
        {
            this.authService = _authService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> LoginUserIn(LoginDTO loginDTO)
        {
            var result = await authService.LoginAsync(loginDTO);
            return Ok(result);
        }


        [HttpPost("register")]

        public async Task<ActionResult<LoginResponse>> RegisterUserDTO(RegisterDTO registerDTO)
        {
            var result = await authService.RegisterAsync(registerDTO);
            return Ok(result);
        }
    }
}