using MediatR;
using Microsoft.EntityFrameworkCore;
using Infrastructure.EFCore;
using CommentService.Domain.Entity;

namespace CommentService.Infrastructure
{
    public class CommentDbContext : BaseDbContext
    {
        public DbSet<Comment> Comments { get; private set; }

        public CommentDbContext(DbContextOptions<CommentDbContext> options, IMediator mediator) : base(options, mediator)
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
