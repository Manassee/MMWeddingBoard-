using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MMWeddingBoard.Domain.Weddings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMWeddingBoard.Infrastructure.Persistence.Configurations
{
    public class WeddingConfiguration : IEntityTypeConfiguration<Wedding>
    {
        public void Configure(EntityTypeBuilder<Wedding> builder)
        {
            builder.ToTable("Weddings");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("wedding_id");

            builder.Property(x => x.Title).HasColumnName("Title");

            // DateOnly -> Postgres DATE
            builder.Property(x => x.EventDate).HasColumnName("event_date").HasColumnType("date");

            builder.Property(x => x.Location).HasColumnName("location").HasMaxLength(300);

            builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired();

            builder.HasIndex(x => x.EventDate);

        }
    }
}
