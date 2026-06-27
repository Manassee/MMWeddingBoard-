using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMWeddingBoard.Shared.Dtos;

namespace MMWedding.Application.Abstractions
{
    public interface IGuestService
    {
        Task<IReadOnlyList<GuestDto>> GetAllAsync(Guid weddingId, CancellationToken ct = default);
        Task<GuestDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<Guid> CreateAsync(CreateGuestRequest request, CancellationToken ct = default);
        Task<bool> UpdateAsync(Guid id, UpdateGuestRequest request, CancellationToken ct = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    }
}
