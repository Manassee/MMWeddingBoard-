using MMWedding.Application.Abstractions;
using MMWeddingBoard.Domain.Guests;
using MMWeddingBoard.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMWeddingBoard.Infrastructure.Repositories
{
    public class GuestRepository : IGuestRepository
    {
        private readonly WeddingDbContext _db;
        public GuestRepository(WeddingDbContext db) => _db = db;

        public async Task<IReadOnlyList<Guest>> GetAllAsync(Guid weddingId, CancellationToken ct = default)
            => await _db.Set<Guest>().Where(g => g.WeddingId == weddingId).ToListAsync(ct);

        public async Task<Guest?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => await _db.Set<Guest>().FindAsync(new object[] { id }, ct);

        public async Task AddAsync(Guest guest, CancellationToken ct = default)
            => await _db.Set<Guest>().AddAsync(guest, ct);

        public async Task DeleteAsync(Guest guest, CancellationToken ct = default)
        {
            _db.Set<Guest>().Remove(guest);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync(CancellationToken ct = default)
            => await _db.SaveChangesAsync(ct);
    }
}
