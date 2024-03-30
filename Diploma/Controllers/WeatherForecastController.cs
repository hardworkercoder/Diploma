using Diploma.Services;
using Microsoft.AspNetCore.Mvc;

namespace Diploma.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IFileParser _fileParser;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IFileParser fileParser)
        {
            _logger = logger;
            _fileParser = fileParser;
        }

        [HttpPost(Name = "UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file received.");

            var data = _fileParser.Parse(file);

            return Ok(data);
        }
    }
}
