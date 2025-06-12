using Microsoft.AspNetCore.Mvc;
using online_course.core.Data;

namespace online_course_api.Controllers
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
        private readonly OnlineCourseDbContext _onlineCourseDbContext;

        // her we are injecting the OnlineCourseDbContext to access the database
        // this is done using Dependency Injection (DI) which is a design pattern that allows us to inject dependencies into a class
        //DI is a way to not allow to create an instance of a class directly, instead we inject the instance of the class into the constructor
        public WeatherForecastController(ILogger<WeatherForecastController> logger, OnlineCourseDbContext onlineCourseDbContext)
        {
            _logger = logger;
            _onlineCourseDbContext = onlineCourseDbContext;
        }

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult GetCourses()
        {
            // Here we are using the injected OnlineCourseDbContext to access the Courses table
            var courses = _onlineCourseDbContext.Courses.ToList();
            return Ok(courses);
        }
    }
}
