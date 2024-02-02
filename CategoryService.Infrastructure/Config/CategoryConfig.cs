using CategoryService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CategoryService.Infrastructure.Config
{
    class CategoryConfig:IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("T_Categories");
            builder.Property(x => x.Name).IsRequired().HasMaxLength(500).IsUnicode();
            builder.HasIndex(x => new { x.Id, x.IsDeleted });
        }
    }
}
