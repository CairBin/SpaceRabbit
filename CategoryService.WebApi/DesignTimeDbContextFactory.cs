using CommonInit;
using Microsoft.EntityFrameworkCore.Design;
using CategoryService.Infrastructure;

namespace CategoryService.WebApi;


public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CategoryDbContext>
{
    public CategoryDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = DbContextOptionsBuilderFactory.Create<CategoryDbContext>();
        return new CategoryDbContext(optionsBuilder.Options, null);
    }
}