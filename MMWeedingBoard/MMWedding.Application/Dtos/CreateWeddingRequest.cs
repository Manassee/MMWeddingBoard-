using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMWedding.Application.Dtos
{
    public record CreateWeddingRequest
    {
        public string Title { get; set; }
        public DateOnly EventDate { get; set; }
        public string? Location { get; set; }
    }
}
