using MediatR;
using Microsoft.EntityFrameworkCore;
using Infrastructure.EFCore;
using CategoryService.Domain.Entity;

namespace CategoryService.Infrastructure
{
    public class CategoryDbContext : BaseDbContext
    {
        public DbSet<Category> Categories { get; private set; }

        public CategoryDbContext(DbContextOptions<CategoryDbContext> options, IMediator mediator) :base(options,mediator)
        {
            this.Database.Migrate();
        }

       protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            builder.EnableSoftDeletionGlobalFilter();
        }

    }
}
