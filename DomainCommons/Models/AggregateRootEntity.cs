using System;

namespace DomainCommons.Models
{
    public record class AggregateRootEntity: BaseEntity, IAggregateRoot, ISoftDelete, IHasCreationTime, IHasDeletionTime, IHasModificationTime
    {
        public bool IsDeleted { get; set; }
        public DateTime Created { get; private set; } = DateTime.Now;
        public DateTime Updated { get; private set; } = DateTime.Now;

        public DateTime? Deleted { get; private set; }
        
        //软删除
        public virtual void SoftDelete()
        {
            this.IsDeleted = true;
            this.Deleted = DateTime.Now;
        }

        public void NotifyUpdated()
        {
            if(DateTime.Now > this.Created)
                this.Updated = DateTime.Now;
        }

        public void SetCreated(DateTime time)
        {
            this.Created = time;
        }

        public void SetUpdated(DateTime time)
        {
            this.Updated= time;
        }

    }
}
