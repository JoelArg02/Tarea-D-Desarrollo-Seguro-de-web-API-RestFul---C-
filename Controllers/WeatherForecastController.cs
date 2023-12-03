using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private static List<WeatherForecast> _weatherForecasts = new List<WeatherForecast>();

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecasts")]
        public IEnumerable<WeatherForecast> Get()
        {
            return _weatherForecasts;
        }

        [HttpGet("{id}", Name = "GetWeatherForecastById")]
        public IActionResult GetById(int id)
        {
            var weatherForecast = _weatherForecasts.FirstOrDefault(w => w.Id == id);
            if (weatherForecast == null)
            {
                return NotFound();
            }
            return Ok(weatherForecast);
        }

        [HttpPost(Name = "CreateWeatherForecast")]
        public IActionResult Create([FromBody] WeatherForecast weatherForecast)
        {
            weatherForecast.Id = _weatherForecasts.Count + 1;
            _weatherForecasts.Add(weatherForecast);
            return CreatedAtRoute("GetWeatherForecastById", new { id = weatherForecast.Id }, weatherForecast);
        }

        [HttpPut("{id}", Name = "UpdateWeatherForecast")]
        public IActionResult Update(int id, [FromBody] WeatherForecast weatherForecast)
        {
            var existingWeatherForecast = _weatherForecasts.FirstOrDefault(w => w.Id == id);
            if (existingWeatherForecast == null)
            {
                return NotFound();
            }

            existingWeatherForecast.Date = weatherForecast.Date;
            existingWeatherForecast.TemperatureC = weatherForecast.TemperatureC;
            existingWeatherForecast.Summary = weatherForecast.Summary;

            return NoContent();
        }

        [HttpDelete("{id}", Name = "D - Eliminar")]
        public IActionResult Delete(int id)
        {
            var existingWeatherForecast = _weatherForecasts.FirstOrDefault(w => w.Id == id);
            if (existingWeatherForecast == null)
            {
                return NotFound();
            }

            _weatherForecasts.Remove(existingWeatherForecast);
            return NoContent();
        }
    }

    public class WeatherForecast
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public string Summary { get; set; }
    }
}
