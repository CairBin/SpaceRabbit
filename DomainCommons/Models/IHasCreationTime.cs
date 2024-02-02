using System;

namespace DomainCommons.Models
{
    public interface IHasCreationTime
    {
        DateTime Created { get; }
    }
}
