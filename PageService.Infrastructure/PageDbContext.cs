using MediatR;
using Microsoft.EntityFrameworkCore;
using Infrastructure.EFCore;
using PageService.Domain.Entity;

namespace PageService.Infrastructure
{
    public class PageDbContext : BaseDbContext
    {
        public DbSet<Page> Pages { get; private set; }
    
        public PageDbContext(DbContextOptions<PageDbContext> options, IMediator mediator) : base(options, mediator)
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
