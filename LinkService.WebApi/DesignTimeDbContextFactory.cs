using CommonInit;
using Microsoft.EntityFrameworkCore.Design;
using LinkService.Infrastructure;

namespace LinkService.WebApi;


public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LinkDbContext>
{
    public LinkDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = DbContextOptionsBuilderFactory.Create<LinkDbContext>();
        return new LinkDbContext(optionsBuilder.Options, null);
    }
}