using MMWedding.Application.Dtos;
using MMWeddingBoard.Domain.Weddings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMWedding.Application.Mapping
{
    public static class WeddingMapper
    {
        public static WeddingDto ToDto(this Wedding w) => new()
        {
            Id = w.Id,
            Title = w.Title,
            EventDate = w.EventDate,
            Location = w.Location,
            CreatedAt = w.CreatedAt,
            UpdatedAt = w.UpdatedAt
        };
    }
}
