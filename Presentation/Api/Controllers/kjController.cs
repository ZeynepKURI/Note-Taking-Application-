using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        // Basit bir hava durumu servisi
        [HttpGet("{city}")]
        [Authorize]
        public IActionResult GetWeather(string city)
        {
            // Örnek hava durumu verisi
            var weather = new
            {
                City = city,
                Temperature = 25,
                Condition = "Sunny"
            };

            return Ok(weather);
        }
    }
}
