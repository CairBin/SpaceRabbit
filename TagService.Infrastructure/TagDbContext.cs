using MediatR;
using Microsoft.EntityFrameworkCore;
using Infrastructure.EFCore;
using TagService.Domain.Entity;


namespace TagService.Infrastructure
{
    public class TagDbContext : BaseDbContext
    {
        public DbSet<Tag> Tags { get; private set; }

        public TagDbContext(DbContextOptions<TagDbContext> options, IMediator mediator) : base(options, mediator)
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
