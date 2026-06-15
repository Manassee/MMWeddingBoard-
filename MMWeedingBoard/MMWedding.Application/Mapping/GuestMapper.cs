using MMWeddingBoard.Domain.Guests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMWeddingBoard.Shared.Dtos;

namespace MMWedding.Application.Mapping
{
    public static class GuestMapper
    {
        public static GuestDto ToDto(this Guest g) => new GuestDto
        {
            Id = g.Id,
            WeddingId = g.WeddingId,
            FirstName = g.FirstName,
            LastName = g.LastName,
            PartySize = g.PartySize,
            ChildrenCount = g.ChildrenCount,
            Status = (MMWeddingBoard.Shared.Dtos.GuestStatus)(int)g.Status,
            CreatedAt = g.CreatedAt,
        };
    }
}
