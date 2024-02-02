using OptionService.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OptionService.Infrastructure.Config
{
    class OptionConfig : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.ToTable("T_Options");
            builder.HasIndex(x => new { x.Id, x.IsDeleted });
        }
    }
}
