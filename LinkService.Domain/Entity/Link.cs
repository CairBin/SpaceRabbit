using DomainCommons.Models;
using LinkService.Domain.Event;


namespace LinkService.Domain.Entity;

public record class Link : AggregateRootEntity, IAggregateRoot,IHasCreationTime,IHasModificationTime
{
    private Link() { }

    public string Title { get; private set; }

    public string? Description { get; private set; }

    public Uri? LogoUrl { get; set; }

    public Uri SiteUrl { get; set; }


    public Link(string title, string? description, Uri? logoUrl, Uri SiteUri)
    {
        this.Title = title;
        this.Description = description;
        this.LogoUrl = logoUrl;
        this.SiteUrl = SiteUri;
        this.AddDomainEvent(new LinkCreatedEvent(this));
    }

    public void EditLink(string title, string? description, Uri? logoUrl, Uri SiteUri)
    {
        this.Title = title;
        this.Description = description;
        this.LogoUrl = logoUrl;
        this.SiteUrl = SiteUri;
        this.NotifyUpdated();
        this.AddDomainEvent(new LinkUpdatedEvent(this));
    }

    public override void SoftDelete()
    {
        base.SoftDelete();
        this.AddDomainEventIfAbsent(new LinkDeletedEvent(this.Id));
    }

}
