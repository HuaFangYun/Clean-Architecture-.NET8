using CA.Application.Samples.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CA.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SampleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSample(AddSampleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
