namespace MMWedding.Application.Dtos
{
    public record WeddingDto
    {
        public Guid Id { get; set; }

        public string BrideName { get; set; } = default!;
        public string GroomName { get; set; } = default!;

        public DateOnly? EventDate { get; init; }

        public string Location { get; init; }

        public DateTimeOffset CreatedAt { get; init; }

        public DateTimeOffset UpdatedAt { get; init; }
    }
}
