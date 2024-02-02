using System;


namespace DomainCommons.Models
{
    public interface IHasModificationTime
    {
        DateTime Updated { get; }

    }
}
