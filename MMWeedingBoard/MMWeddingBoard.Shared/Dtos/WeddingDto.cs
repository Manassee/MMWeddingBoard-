namespace MMWeddingBoard.Shared.Dtos
{
    public record WeddingDto
    {
        public Guid Id { get; set; }

        public string BrideName { get; set; } = default!;
        public string GroomName { get; set; } = default!;

        public DateOnly? EventDate { get; set; }

        public string Location { get; set; }

        public DateTimeOffset CreatedAt { get; init; }

        public DateTimeOffset UpdatedAt { get;  set; }
    }
}
