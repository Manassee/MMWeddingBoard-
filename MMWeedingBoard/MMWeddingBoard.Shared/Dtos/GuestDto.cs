using MMWeddingBoard.Domain.Guests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMWeddingBoard.Shared.Dtos
{
    public record GuestDto
    {
        public Guid Id { get; set; }
        public Guid WeddingId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public int PartySize { get;     set; }
        public int ChildrenCount { get; set; }
        public GuestStatus Status { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
