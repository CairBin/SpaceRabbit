using System;
using DomainCommons.Models;

using PageService.Domain.Event;
using PageService.Domain.ValueObject;

namespace PageService.Domain.Entity;

public record class Page : AggregateRootEntity, IAggregateRoot, IHasCreationTime, IHasModificationTime
{
    private Page() { }

    public string Title { get; private set; }
    public string Content { get; private set; }

    public string PageName { get;init; }

    public AdditionalField OptionField { get; private set; }


    public Page(string title, string content, string pageName, AdditionalField optionField)
    {
        Title = title;
        Content = content;
        PageName = pageName;
        OptionField = optionField;
        this.AddDomainEvent(new PageCreatedEvent(this));
    }

    public Page EditPage(string title, string content, AdditionalField optionField)
    {
        this.Title = title;
        this.Content = content;
        this.OptionField = optionField;
        this.NotifyUpdated();
        this.AddDomainEvent(new PageUpdatedEvent(this));
        return this;
    }

    public override void SoftDelete()
    {
        base.SoftDelete();
        this.AddDomainEvent(new PageDeletedEvent(this.Id));
    }

    public Page SetField(AdditionalField field)
    {
        this.OptionField = field;
        this.NotifyUpdated();
        this.AddDomainEvent(new PageUpdatedEvent(this));
        return this;
    }

}