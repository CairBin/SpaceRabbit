using CommonInit;
using Microsoft.EntityFrameworkCore.Design;
using PageService.Infrastructure;

namespace PageService.WebApi;


public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PageDbContext>
{
    public PageDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = DbContextOptionsBuilderFactory.Create<PageDbContext>();
        return new PageDbContext(optionsBuilder.Options, null);
    }
}