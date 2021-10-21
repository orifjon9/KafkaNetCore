using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProducerApi.Providers;

namespace ProducerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IProducerService _producerService;
        public MessagesController(IProducerService producerService)
        {
            _producerService = producerService;
        }

        // POST api/messages/send
        [HttpPost("send")]
        public async Task<IActionResult> PostAsync([FromQuery] string message)
        {
            await _producerService.SendAsync(message);
            return Accepted();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return NoContent();
        }
    }
}
