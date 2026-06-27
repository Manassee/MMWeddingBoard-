using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMWeddingBoard.Shared.Dtos
{
    public record CreateWeddingRequest
    {
        public string BrideName { get; set; } = default!;
        public string GroomName { get; set; } = default!;
        public DateOnly EventDate { get; set; }
        public string? Location { get; set; }
    }
}
