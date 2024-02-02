using CommonInit;
using ArticleService.Infrastructure;
using Microsoft.EntityFrameworkCore.Design;

namespace IdentityService.Api
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ArticleDbContext>
    {
        public ArticleDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = DbContextOptionsBuilderFactory.Create<ArticleDbContext>();
            return new ArticleDbContext(optionsBuilder.Options, null);
        }
    }
}
