using CafeOps.Logic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CafeOps.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CafesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CafesController(IMediator mediator)
        {
            _mediator = mediator;
        }        

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? location)
        {
            var result = await _mediator.Send(new GetCafesQuery(location));
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCafeCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(Create), new { id = id }, id);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCafe([FromBody] UpdateCafeCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCafe(Guid id)
        {
            await _mediator.Send(new DeleteCafeCommand(id));
            return NoContent();
        }

    }
}
