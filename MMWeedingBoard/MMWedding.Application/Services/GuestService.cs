using MMWedding.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMWedding.Application.Mapping;
using MMWeddingBoard.Domain.Guests;
using MMWeddingBoard.Shared.Dtos;

namespace MMWedding.Application.Services
{
    public class GuestService : IGuestService
    {
        private readonly IGuestRepository _repository;
        public GuestService(IGuestRepository repository) => _repository = repository;

        public async Task<IReadOnlyList<GuestDto>> GetAllAsync(Guid weddingId, CancellationToken ct = default)
        {
            var guests = await _repository.GetAllAsync(weddingId, ct);
            return guests
                .OrderBy(g => g.LastName)
                .ThenBy(g => g.FirstName)
                .Select(g => g.ToDto())
                .ToList();
        }

        public async Task<GuestDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var guest = await _repository.GetByIdAsync(id, ct);
            return guest?.ToDto();
        }

        public async Task<Guid> CreateAsync(CreateGuestRequest request, CancellationToken ct = default)
        {
            var guest = new Guest(
                request.WeddingId,
                request.FirstName,
                request.LastName,
                request.PartySize,
                request.ChildrenCount);
            await _repository.AddAsync(guest, ct);
            await _repository.SaveChangesAsync(ct);
            return guest.Id;
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateGuestRequest request, CancellationToken ct = default)
        {
            var guest = await _repository.GetByIdAsync(id, ct);
            if (guest is null) return false;
            guest.SetName(request.FirstName, request.LastName);
            guest.SetPartySize(request.PartySize);
            guest.SetChildrenCount(request.ChildrenCount);
            guest.ChangeStatus((MMWeddingBoard.Domain.Guests.GuestStatus)(int)request.Status);
            await _repository.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var guest = await _repository.GetByIdAsync(id, ct);
            if (guest is null) return false;
            await _repository.DeleteAsync(guest, ct);
            await _repository.SaveChangesAsync(ct);
            return true;
        }
    }
}
