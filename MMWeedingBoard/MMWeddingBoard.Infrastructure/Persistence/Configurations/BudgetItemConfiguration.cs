using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MMWeddingBoard.Domain.Budgets;

namespace MMWeddingBoard.Infrastructure.Persistence.Configurations;

public sealed class BudgetItemConfiguration : IEntityTypeConfiguration<BudgetItem>
{
    public void Configure(EntityTypeBuilder<BudgetItem> b)
    {
        b.ToTable("budget_items");

        b.HasKey(x => x.Id);

        b.Property(x => x.Id)
            .HasColumnName("budget_item_id");

        b.Property(x => x.BudgetId)
            .HasColumnName("budget_id")
            .IsRequired();

        b.Property(x => x.Title)
            .HasColumnName("title")
            .IsRequired()
            .HasMaxLength(200);

        b.Property(x => x.Amount)
            .HasColumnName("amount")
            .HasColumnType("numeric(12,2)")
            .IsRequired();

        b.Property(x => x.Type)
            .HasColumnName("type")
            .HasConversion<int>()
            .IsRequired();

        b.Property(x => x.PaymentStatus)
            .HasColumnName("payment_status")
            .HasConversion<int>()
            .IsRequired();

        b.Property(x => x.PaidAt)
            .HasColumnName("paid_at");

        b.Property(x => x.VendorName)
            .HasColumnName("vendor_name")
            .HasMaxLength(200);

        b.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        b.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        // ✅ KEINE Relationship hier definieren (kommt aus BudgetConfiguration)
        // Sonst kann EF daraus 2 Beziehungen machen -> BudgetId1

        b.HasIndex(x => x.BudgetId);
        b.HasIndex(x => new { x.BudgetId, x.Type });
    }
}