using CommonInit;
using Microsoft.EntityFrameworkCore.Design;
using TagService.Infrastructure;

namespace TagService.WebApi;


public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TagDbContext>
{
    public TagDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = DbContextOptionsBuilderFactory.Create<TagDbContext>();
        return new TagDbContext(optionsBuilder.Options, null);
    }
}