using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMWeddingBoard.Shared.Dtos
{
    public record UpdateGuestRequest
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public int PartySize { get; set; } = 1;
        public int ChildrenCount { get; set; } = 0;
        public GuestStatus Status { get; set; }
    }
}
