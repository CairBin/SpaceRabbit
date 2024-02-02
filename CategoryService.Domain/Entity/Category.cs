using DomainCommons.Models;
using CategoryService.Domain.Event;

namespace CategoryService.Domain.Entity
{
    public record class Category : AggregateRootEntity, IAggregateRoot
    {
        /// <summary>
        /// Default constructor for EF Core
        /// </summary>
        private Category(){}

        public string Name { get;private set; }

        public Category(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.AddDomainEvent(new CategoryCreatedEvent(this));
        }

        public Category ChangeName(string name)
        {
            this.Name=name;
            this.NotifyUpdated();
            this.AddDomainEvent(new CategoryUpdatedEvent(this));
            return this;
        }

        public override void SoftDelete()
        {
            base.SoftDelete();
            this.AddDomainEvent(new CategoryDeletedEvent(this.Id));
        }

    }
}
