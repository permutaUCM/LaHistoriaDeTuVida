using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LHDTV.Service;

namespace LHDTV.Controllers
{
    [ApiController]
    [Route("api/weather")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMailService Mail;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMailService mail)
        {
            _logger = logger;
            Mail = mail;
        }

        [HttpGet("GetAll")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("sendEmail")]
        public ActionResult email()
        {

            Mail.SendEmail("Pedro", "auryn.noreply@gmail.com", "David", "pedagova@gmail.com", "Hola caracola", string.Format(@"<p>Hola Pedro</p> 
            <p>Tu cuenta ha sido creada correctamente, accede al siguiente enlace para comenzar tus nuevos álbums<a href='https://localhost:4200'>aquí</a></p>"));
            return Ok();
        }
    }
}
