using MediatR;
using Microsoft.EntityFrameworkCore;
using Infrastructure.EFCore;
using LinkService.Domain.Entity;

namespace LinkService.Infrastructure
{
    public class LinkDbContext : BaseDbContext
    {
        public DbSet<Link> Links { get; private set; }

        public LinkDbContext(DbContextOptions<LinkDbContext> options, IMediator mediator) : base(options, mediator)
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
