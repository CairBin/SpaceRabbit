using MediatR;
using Microsoft.EntityFrameworkCore;
using Infrastructure.EFCore;
using OptionService.Domain.Entity;

namespace OptionService.Infrastructure
{
    public class OptionDbContext : BaseDbContext
    {
        public DbSet<Option> Options { get; private set; }

        public OptionDbContext(DbContextOptions<OptionDbContext> options, IMediator mediator) : base(options, mediator)
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
