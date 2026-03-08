namespace MMWeddingBoard.Shared.Dtos
{
    public record WeddingDto
    {
        public Guid Id { get; init; }

        public string Title { get; init; } = default!;

        public DateOnly? EventDate { get; init; }

        public string Location { get; init; }

        public DateTimeOffset CreatedAt { get; init; }

        public DateTimeOffset UpdatedAt { get; init; }
    }
}
