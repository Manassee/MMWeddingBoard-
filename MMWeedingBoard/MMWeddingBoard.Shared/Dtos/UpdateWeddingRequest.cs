using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMWeddingBoard.Shared.Dtos
{
    public class UpdateWeddingRequest
    {
        public string Title { get; set; } = default!;
        public DateOnly EventDate { get; set; }
        public string? Location { get; set; }
    }
}
