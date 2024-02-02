using System;


namespace DomainCommons.Models
{
    public interface IHasDeletionTime
    {
        DateTime? Deleted { get; }
    }
}
