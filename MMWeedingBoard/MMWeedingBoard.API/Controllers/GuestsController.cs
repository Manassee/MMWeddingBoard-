using Microsoft.AspNetCore.Mvc;
using MMWedding.Application.Abstractions;
using MMWeddingBoard.Shared.Dtos;


namespace MMWeddingBoard.API.Controllers
{
    [Route("api/weddings/{weddingId:guid}/guests")]
    [ApiController]
    public class GuestsController : ControllerBase
    {
        private readonly IGuestService _service;
        public GuestsController(IGuestService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GuestDto>>> GetAll(Guid weddingId, CancellationToken ct)
            => Ok(await _service.GetAllAsync(weddingId, ct));

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GuestDto>> GetById(Guid weddingId, Guid id, CancellationToken ct)
        {
            var dto = await _service.GetByIdAsync(id, ct);
            return dto is null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(Guid weddingId,
            [FromBody] CreateGuestRequest request, CancellationToken ct)
        {
            request = request with { WeddingId = weddingId };
            var id = await _service.CreateAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { weddingId, id }, id);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid weddingId, Guid id,
            [FromBody] UpdateGuestRequest request, CancellationToken ct)
        {
            var ok = await _service.UpdateAsync(id, request, ct);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid weddingId, Guid id, CancellationToken ct)
        {
            var ok = await _service.DeleteAsync(id, ct);
            return ok ? NoContent() : NotFound();
        }
    }
}
