using CommentService.Domain;
using DomainCommons.Models;
using CommentService.Domain.ValueObject;
using CommentService.Domain.Event;

namespace CommentService.Domain.Entity;

public record class Comment: AggregateRootEntity, IAggregateRoot, IHasCreationTime,IHasModificationTime
{

    public enum CommentStatus
    {
        Normal = 0,
        Private = 1,
        Spam = 2
    }

    public enum CommentType
    {
        Article = 0,
        Page = 1
    }

    /// <summary>
    /// 留给EF core 工厂
    /// </summary>
    private Comment() { }

    public Guid ContentId { get; private set; }
    /// <summary>
    /// 0是文章评论 1是页面评论
    /// </summary>
    public int Type { get; private set; }  

    public int Status { get; private set; }

    public Guid? Parent { get;private set; }

    public string Text { get; private set; }

    public Submitter Submitter { get; private set; }

    public Guid? AuthorId { get; private set; }


    public void ChangeStatus(int status)
    {
        this.Status = status;
        this.NotifyUpdated();
        this.AddDomainEvent(new CommentUpdatedEvent(this));
    }

    public void ChangeType(int type)
    {
        this.Type = type;
        this.NotifyUpdated();
        this.AddDomainEvent(new CommentUpdatedEvent(this));
    }

    public void EditComment(string text)
    {
        this.Text = text;
        this.NotifyUpdated();
        this.AddDomainEvent(new CommentUpdatedEvent(this));
    }

    public override void SoftDelete()
    {
        base.SoftDelete();
        this.AddDomainEvent(new CommentDeletedEvent(this.Id));
    }





    public class Builder
    {
        private Guid contentId;
        private int type;
        private int status;
        private Guid? parent;
        private string text;
        private Submitter submitter;
        private Guid? authorId;

        public Builder ContentId(Guid value)
        {
            contentId = value;
            return this;
        }

        public Builder Type(int value)
        {
            this.type = value;
            return this;
        }

        public Builder Parent(Guid? value)
        {
            this.parent = value;
            return this;
        }

        public Builder Status(int value)
        {
            this.status = value;
            return this;
        }

        public Builder Text(string value)
        {
            this.text = value;
            return this;
        }

        public Builder Submitter(Submitter value)
        {
            this.submitter = value;
            return this;
        }

        public Builder AuthorId(Guid? value)
        {
            this.authorId = value;
            return this;
        }

        public Builder SetPageComment()
        {
            this.type = (int)Comment.CommentType.Page;
            return this;
        }

        public Builder SetArticleComment()
        {
            this.type = (int)Comment.CommentType.Article;
            return this;
        }

        public Builder SetNormal()
        {
            this.status = (int)Comment.CommentStatus.Normal;
            return this;
        }

        public Builder SetPrivate()
        {
            this.status= (int)Comment.CommentStatus.Private;
            return this;
        }

        public Builder SetSpam()
        {
            this.status = (int)Comment.CommentStatus.Spam;
            return this;
        }


        public Comment Build()
        {
            Comment comment = new Comment();
            comment.ContentId = contentId;
            comment.Type = type;
            comment.Status = status;
            comment.Text = text;
            comment.Submitter = submitter;
            comment.AuthorId = authorId;
            comment.Parent = parent;
            comment.AddDomainEvent(new CommentCreatedEvent(comment));
            return comment;
        }
    }
    

}
