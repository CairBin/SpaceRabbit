using CommentService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CategoryService.Infrastructure.Config
{
    class CategoryConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("T_Comments");
            builder.OwnsOne(x => x.Submitter, sub =>
              {
                  sub.Property(x => x.Name).IsUnicode(true).HasMaxLength(100);
                  sub.Property(x => x.Email).HasMaxLength(100);
                  sub.Property(x => x.SiteUrl).HasMaxLength(1000);
                  sub.Property(x => x.Agent).IsUnicode(true).HasMaxLength(1000);
              });
            builder.HasIndex(x => new { x.IsDeleted, x.Id, x.Type, x.Status });
        }
    }
}
