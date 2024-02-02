using MediatR;
using Microsoft.EntityFrameworkCore;
using Infrastructure.EFCore;
using ArticleService.Domain.Entity;

namespace ArticleService.Infrastructure
{
    public class ArticleDbContext : BaseDbContext
    {
        public DbSet<Article> Articles { get; private set; }

        public ArticleDbContext(DbContextOptions<ArticleDbContext> options, IMediator mediator) : base(options,mediator)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            builder.EnableSoftDeletionGlobalFilter();
        }


    }
}
