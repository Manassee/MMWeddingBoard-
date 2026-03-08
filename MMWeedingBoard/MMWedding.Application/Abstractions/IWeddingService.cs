using MMWedding.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMWedding.Application.Abstractions
{
    /// <summary>
    /// Defines methods for retrieving and updating wedding information asynchronously.
    /// </summary>
    /// <remarks>Implementations of this interface provide operations to access and modify wedding data,
    /// typically in a persistent store. All methods are asynchronous and support cancellation via a <see
    /// cref="CancellationToken"/> parameter.</remarks>
    public interface IWeddingService
    {
        Task<IReadOnlyList<WeddingDto>> GetAllAsync(CancellationToken ct = default);
        Task<WeddingDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<bool> UpdateAsync(Guid id, UpdateWeddingRequest request, CancellationToken ct = default);
        Task<Guid> CreateAsync(CreateWeddingRequest request, CancellationToken ct = default);
    }
}
