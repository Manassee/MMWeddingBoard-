using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MMWeddingBoard.Domain.Guests;
using MMWeddingBoard.Domain.Weddings;

namespace MMWeddingBoard.Infrastructure.Persistence.Configurations;

public sealed class GuestConfiguration : IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> b)
    {
        b.ToTable("guests");

        b.HasKey(x => x.Id);
        b.Property(x => x.Id).HasColumnName("guest_id");

        b.Property(x => x.WeddingId).HasColumnName("wedding_id").IsRequired();

        b.Property(x => x.FirstName).HasColumnName("first_name").IsRequired().HasMaxLength(120);
        b.Property(x => x.LastName).HasColumnName("last_name").IsRequired().HasMaxLength(120);

        b.Property(x => x.PartySize).HasColumnName("party_size").IsRequired();
        b.Property(x => x.ChildrenCount).HasColumnName("children_count").IsRequired();

        b.Property(x => x.Status)
            .HasColumnName("status")
            .HasConversion<int>()
            .IsRequired();

        b.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        b.Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired();

        // FK ohne Navigation Properties
        b.HasOne<Wedding>()
            .WithMany()
            .HasForeignKey(x => x.WeddingId)
            .OnDelete(DeleteBehavior.Cascade);

        b.HasIndex(x => x.WeddingId);
        b.HasIndex(x => new { x.WeddingId, x.LastName, x.FirstName });
    }
}
