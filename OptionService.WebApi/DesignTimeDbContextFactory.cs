using CommonInit;
using Microsoft.EntityFrameworkCore.Design;
using OptionService.Infrastructure;

namespace OptionService.WebApi;


public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OptionDbContext>
{
    public OptionDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = DbContextOptionsBuilderFactory.Create<OptionDbContext>();
        return new OptionDbContext(optionsBuilder.Options, null);
    }
}