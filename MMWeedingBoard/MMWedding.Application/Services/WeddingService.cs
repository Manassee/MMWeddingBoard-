using MMWedding.Application.Abstractions;
using MMWedding.Application.Dtos;
using MMWedding.Application.Mapping;
using MMWeddingBoard.Domain.Weddings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MMWedding.Application.Services
{
    public class WeddingService : IWeddingService
    {
        private readonly IWeddingRepository _repository;
        public WeddingService(IWeddingRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> CreateAsync(CreateWeddingRequest request, CancellationToken ct = default)
        {
            Wedding wedding = new Wedding(
                request.Title,
                request.EventDate,
                request.Location);

            await _repository.AddAsync(wedding, ct);
            await _repository.SaveChangesAsync(ct);

            return wedding.Id;  
        }

        public async Task<IReadOnlyList<WeddingDto>> GetAllAsync(CancellationToken ct = default)
        {
            var weddings = await _repository.GetAllAsync(ct);

            return weddings
                .OrderBy(x => x.EventDate)
                .Select(x => x.ToDto())
                .ToList();
        }

        public async Task<WeddingDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _repository.GetByIdAsync(id, ct);
            return entity?.ToDto();
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateWeddingRequest request, CancellationToken ct = default)
        {
            var wedding = await _repository.GetForUpdateAsync(id, ct);

            if (wedding is null)
                return false;

            wedding.Rename(request.Title);
            wedding.SetLocation(request.Location);
            wedding.SetEventDate(request.EventDate);

            await _repository.SaveChangesAsync(ct);

            return true;
        }
    }
}
