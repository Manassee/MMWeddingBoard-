using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MMWeddingBoard.Domain.Tasks;
using MMWeddingBoard.Domain.Weddings;

namespace MMWeddingBoard.Infrastructure.Persistence.Configurations;

public sealed class WeddingTaskConfiguration : IEntityTypeConfiguration<WeddingTask>
{
    public void Configure(EntityTypeBuilder<WeddingTask> b)
    {
        b.ToTable("tasks");

        b.HasKey(x => x.Id);
        b.Property(x => x.Id).HasColumnName("task_id");

        b.Property(x => x.WeddingId).HasColumnName("wedding_id").IsRequired();

        b.Property(x => x.Title).HasColumnName("title").IsRequired().HasMaxLength(200);
        b.Property(x => x.Description).HasColumnName("description");

        b.Property(x => x.DueDate).HasColumnName("due_date").HasColumnType("date");

        b.Property(x => x.Status).HasColumnName("status").HasConversion<int>().IsRequired();
        b.Property(x => x.Priority).HasColumnName("priority").HasConversion<int>().IsRequired();

        b.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        b.Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired();

        b.HasOne<Wedding>()
            .WithMany()
            .HasForeignKey(x => x.WeddingId)
            .OnDelete(DeleteBehavior.Cascade);

        b.HasIndex(x => x.WeddingId);
        b.HasIndex(x => new { x.WeddingId, x.Status });
        b.HasIndex(x => x.DueDate);
    }
}
