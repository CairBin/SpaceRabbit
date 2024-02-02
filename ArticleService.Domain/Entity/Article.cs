using System;
using DomainCommons.Models;
using ArticleService.Domain.ValueObject;
using ArticleService.Domain.Event;

namespace ArticleService.Domain.Entity
{
    public record class Article: AggregateRootEntity,IAggregateRoot, IHasCreationTime,IHasModificationTime
    {
        public string Title { get; private set; }
        public string Content { get; private set; }

        public long ViewNumber { get; private set; }

        public Guid AuthorId { get; init; }

        public Guid? CategoryId { get; private set; }

        public Uri? CoverUrl { get; private set; }

        public bool IsVisible { get; private set; }
        public Password? HashPassword { get; private set; }

        public AdditionalField OptionField { get; private set; }


        //默认构造留给EF Core
        private Article() { }

        public Article(string title,string content,Guid authorId, Guid? categoryId, Password? password, AdditionalField field, DateTime created, Uri? coverUrl)
        {
            this.Title = title;
            this.Content = content;
            this.ViewNumber = 0;
            this.AuthorId = authorId;
            this.CategoryId = categoryId;
            this.IsVisible = true;
            this.OptionField = field;
            this.CoverUrl = null;
            this.HashPassword = password;
            this.SetCreated(created);
            this.SetUpdated(created);
            this.CoverUrl = coverUrl;
            this.AddDomainEvent(new ArticleCreatedEvent(this));
        }

        public Article EditArticle(string title,string content, AdditionalField field)
        {
            this.Title=title;
            this.Content=content;
            this.OptionField=field;
            this.NotifyUpdated();
            this.AddDomainEvent(new ArticleUpdatedEvent(this));
            return this;
        }

        public Article ChangeType(Guid? categoryId)
        {
            this.CategoryId= categoryId;
            this.NotifyUpdated();
            this.AddDomainEvent(new ArticleUpdatedEvent(this));
            return this;
        }

        public Article Hide()
        {
            this.IsVisible = false;
            this.NotifyUpdated();
            this.AddDomainEvent(new ArticleUpdatedEvent(this));
            return this;
        }

        public Article Show()
        {
            this.IsVisible = true;
            this.NotifyUpdated();
            this.AddDomainEvent(new ArticleUpdatedEvent(this));
            return this;
        }

        public Article SetSecret(Password password)
        {
            this.HashPassword = password;
            this.NotifyUpdated();
            this.AddDomainEvent(new ArticleUpdatedEvent(this));
            return this;
        }

        public Article CancelPassword()
        {
            this.HashPassword=null;
            this.NotifyUpdated();
            this.AddDomainEvent(new ArticleUpdatedEvent(this));
            return this;
        }

        public Article CancelSecret()
        {
            this.HashPassword = null;
            this.NotifyUpdated();
            this.AddDomainEvent(new ArticleUpdatedEvent(this));
            return this;
        }

        public Article MarkDefaultCategory()
        {
            this.CategoryId = null;
            this.NotifyUpdated();
            this.AddDomainEvent(new ArticleUpdatedEvent(this));
            return this;
        }

        public override void SoftDelete()
        {
            base.SoftDelete();
            this.AddDomainEvent(new ArticleDeletedEvent(this.Id));
        }

        public Article AddViewNumber()
        {
            this.ViewNumber += 1;
            this.AddDomainEventIfAbsent(new ArticleUpdatedEvent(this));
            return this;
        }

        public Article SetCover(Uri? url)
        {
            this.CoverUrl = url;
            this.NotifyUpdated();
            this.AddDomainEvent(new ArticleUpdatedEvent(this));
            return this;
        }

    }
}
