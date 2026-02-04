using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MMWeddingBoard.Domain.Budgets;
using MMWeddingBoard.Domain.Weddings;

namespace MMWeddingBoard.Infrastructure.Persistence.Configurations;

public sealed class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> b)
    {
        b.ToTable("budgets");

        b.HasKey(x => x.Id);
        b.Property(x => x.Id).HasColumnName("budget_id");

        b.Property(x => x.WeddingId).HasColumnName("wedding_id").IsRequired();

        b.Property(x => x.Category).HasColumnName("category").IsRequired().HasMaxLength(150);
        b.Property(x => x.PlannedAmount).HasColumnName("planned_amount").HasColumnType("numeric(12,2)").IsRequired();

        b.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        b.Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired();

        b.HasOne<Wedding>()
            .WithMany()
            .HasForeignKey(x => x.WeddingId)
            .OnDelete(DeleteBehavior.Cascade);

        b.HasIndex(x => x.WeddingId);
        b.HasIndex(x => new { x.WeddingId, x.Category }).IsUnique();
    }
}
