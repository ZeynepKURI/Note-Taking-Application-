
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthService authService;

        public UserController(IAuthService _authService)
        {
            authService = _authService;
        }


        public async Task<ActionResult<LoginResponse>> LoginAsync(LoginDTO loginDTO)
        {
            var result = await User.LoginUserAsync(loginDTO);
        }
    }
}