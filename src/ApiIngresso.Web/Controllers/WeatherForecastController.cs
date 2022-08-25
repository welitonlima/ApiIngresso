using Microsoft.AspNetCore.Mvc;

namespace ApiIngresso.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public WeatherForecastController()
        {
        }

        [HttpGet]
        public string Get()
        {
            return "ok";
        }
    }
}
