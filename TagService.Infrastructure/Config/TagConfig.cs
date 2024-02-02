using TagService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TagService.Infrastructure.Config;

class TagConfig : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("T_Tags");
        builder.HasIndex(x => new { x.Id, x.IsDeleted });
    }
}
