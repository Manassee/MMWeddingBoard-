using MMWeddingBoard.Domain.Guests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMWeddingBoard.Domain.Guests;

namespace MMWedding.Application.Abstractions
{
    public interface IGuestRepository
    {
        Task<IReadOnlyList<Guest>> GetAllAsync(Guid weddingId, CancellationToken ct = default);
        Task<Guest?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task AddAsync(Guest guest, CancellationToken ct = default);
        Task SaveChangesAsync(CancellationToken ct = default);
        Task DeleteAsync(Guest guest, CancellationToken ct = default);
    }
}
