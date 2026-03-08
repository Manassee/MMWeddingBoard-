using Microsoft.AspNetCore.Mvc;
using MMWedding.Application.Abstractions;
using MMWedding.Application.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MMWeddingBoard.API.Controllers
{
    [Route("api/weddings")]
    [ApiController]
    public class WeddingsController : ControllerBase
    {
        private readonly IWeddingService _service;

        public WeddingsController(IWeddingService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<WeddingDto>>> GetAll(CancellationToken ct)
            => Ok(await _service.GetAllAsync(ct));

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<WeddingDto>> GetById(Guid id, CancellationToken ct)
        {
            var dto = await _service.GetByIdAsync(id, ct);
            return dto is null ? NotFound() : Ok(dto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWeddingRequest request, CancellationToken ct)
        {
            var ok = await _service.UpdateAsync(id, request, ct);
            return ok ? NoContent() : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateWeddingRequest request, CancellationToken ct)
        {
            var id = await _service.CreateAsync(request, ct);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                id);
        }
    }
}
