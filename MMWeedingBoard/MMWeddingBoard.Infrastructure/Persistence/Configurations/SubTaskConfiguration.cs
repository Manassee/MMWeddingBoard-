using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MMWeddingBoard.Domain.Tasks;

namespace MMWeddingBoard.Infrastructure.Persistence.Configurations;

public sealed class SubTaskConfiguration : IEntityTypeConfiguration<SubTask>
{
    public void Configure(EntityTypeBuilder<SubTask> b)
    {
        b.ToTable("subtasks");

        b.HasKey(x => x.Id);
        b.Property(x => x.Id).HasColumnName("subtask_id");

        b.Property(x => x.TaskId).HasColumnName("task_id").IsRequired();

        b.Property(x => x.Title).HasColumnName("title").IsRequired().HasMaxLength(200);
        b.Property(x => x.IsDone).HasColumnName("is_done").IsRequired();
        b.Property(x => x.OrderIndex).HasColumnName("order_index").IsRequired();

        b.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        b.Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired();

        b.HasOne<WeddingTask>()
            .WithMany()
            .HasForeignKey(x => x.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        b.HasIndex(x => x.TaskId);
        b.HasIndex(x => new { x.TaskId, x.OrderIndex }).IsUnique();
    }
}
