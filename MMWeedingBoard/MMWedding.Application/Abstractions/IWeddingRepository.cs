using MMWeddingBoard.Domain.Weddings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMWedding.Application.Abstractions
{
    public interface IWeddingRepository
    {
        Task<List<Wedding>> GetAllAsync(CancellationToken ct = default);
        Task<Wedding?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<Wedding?> GetForUpdateAsync(Guid id, CancellationToken ct = default);

        Task AddAsync(Wedding wedding, CancellationToken ct = default);
        Task SaveChangesAsync(CancellationToken ct = default);
    }
}
