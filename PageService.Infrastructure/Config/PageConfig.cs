using PageService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PageService.Infrastructure.Config;

class PageConfig : IEntityTypeConfiguration<Page>
{
    public void Configure(EntityTypeBuilder<Page> builder)
    {
        builder.ToTable("T_Pages");
        builder.Property(x => x.PageName).IsRequired();
        builder.Property(x=>x.Title).IsRequired().HasMaxLength(500);
        builder.OwnsOne(x => x.OptionField, field =>
        {
            field.Property(x => x.PublicField).IsUnicode();
            field.Property(x => x.PrivateField).IsUnicode();
        });
        builder.HasIndex(x => new { x.Id, x.IsDeleted });
    }
}
