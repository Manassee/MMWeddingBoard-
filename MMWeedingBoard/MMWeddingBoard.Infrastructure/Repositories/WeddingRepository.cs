using Microsoft.EntityFrameworkCore;
using MMWedding.Application.Abstractions;
using MMWeddingBoard.Domain.Weddings;
using MMWeddingBoard.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMWeddingBoard.Infrastructure.Repositories
{
    public class WeddingRepository : IWeddingRepository
    {
        private readonly WeddingDbContext _db;

        public WeddingRepository(WeddingDbContext db) => _db = db;

        public async Task AddAsync(Wedding wedding, CancellationToken ct = default)
        {
            await _db.Weddings.AddAsync(wedding, ct);
        }

        public Task<List<Wedding>> GetAllAsync(CancellationToken ct = default)
            => _db.Weddings.AsNoTracking().ToListAsync(ct);

        public Task<Wedding?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => _db.Weddings.FirstOrDefaultAsync(x => x.Id == id, ct);

        public Task<Wedding?> GetForUpdateAsync(Guid id, CancellationToken ct = default)
            => _db.Weddings.FirstOrDefaultAsync(x => x.Id == id, ct);

        public Task SaveChangesAsync(CancellationToken ct = default)
            => _db.SaveChangesAsync(ct);
    }
}
