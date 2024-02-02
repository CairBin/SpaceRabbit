using ArticleService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ArticleService.Infrastructure.Config
{
    class ArticleConfig: IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("T_Articles");
            builder.HasKey(x => x.Id).IsClustered(false);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(500).IsUnicode();
            builder.Property(x => x.Content).IsRequired().IsUnicode();
            builder.OwnsOne(x => x.HashPassword, pwd =>
              {
                  pwd.Property(x => x.Salt).IsUnicode(false);
                  pwd.Property(x=>x.HashValue).IsUnicode(false);
              });
            builder.OwnsOne(x => x.OptionField, field =>
              {
                  field.Property(x => x.PublicField).IsUnicode(true);
                  field.Property(x => x.PrivateField).IsUnicode(true);
              });
            builder.HasIndex(x => new { x.Id, x.IsDeleted });

        }
    }
}
