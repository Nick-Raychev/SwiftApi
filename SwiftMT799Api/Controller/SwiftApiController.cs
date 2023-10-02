using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;

namespace SwiftMT799Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SwiftController : ControllerBase
    {
        private readonly ILogger<SwiftController> _logger;
        private readonly SwiftRepository _swiftRepository;

        public SwiftController(ILogger<SwiftController> logger, SwiftRepository swiftRepository)
        {
            _logger = logger;
            _swiftRepository = swiftRepository;
        }

        [HttpPost]
        public IActionResult Post([FromBody] string rawMessage)
        {
            try
            {
                var parser = new SwiftMessageParser();
                var swiftMessage = parser.Parse(rawMessage);

                _swiftRepository.SaveSwiftMessage(swiftMessage);

                _logger.LogInformation($"Received Swift Message:\n{swiftMessage}");
                return Ok("Message processed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing Swift Message: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}