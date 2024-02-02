using System;
using DomainCommons.Models;
using TagService.Domain.Event;


namespace TagService.Domain.Entity;

public record Tag : AggregateRootEntity, IAggregateRoot, IHasCreationTime, IHasModificationTime
{
    private Tag() { }

    public string TagName { get; private set; }
    public Guid ArticleId { get; private set; }

    public Tag(string TagName, Guid articleId)
    {
        this.TagName = TagName;
        ArticleId = articleId;
        this.AddDomainEvent(new TagCreatedEvent(this));
    }


    public override void SoftDelete()
    {
        base.SoftDelete();
        this.AddDomainEventIfAbsent(new TagDeletedEvent(this.Id));
    }
}
