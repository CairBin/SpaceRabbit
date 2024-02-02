using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LinkService.Domain.Entity;

namespace LinkService.Infrastructure.Config
{
    public class LinkConfig: IEntityTypeConfiguration<Link>
    {
        public void Configure(EntityTypeBuilder<Link> builder)
        {
            builder.ToTable("T_Links");
            builder.HasIndex(x => new { x.Id, x.IsDeleted });
            builder.Property(x=>x.Title).HasMaxLength(500).IsUnicode(true);
            builder.Property(x=>x.Description).HasMaxLength(1000).IsUnicode(true);
        }
    }
}
