using ArticleService.Domain.Event;
using EventBus;
using MediatR;

namespace ArticleService.Admin.WebApi.EventHandler;

public class ArticleCreatedEventHandler : INotificationHandler<ArticleCreatedEvent>
{
    private readonly IEventBus _eventBus;
    public ArticleCreatedEventHandler(IEventBus eventBus)
    {
        this._eventBus = eventBus;
    }

    public Task Handle(ArticleCreatedEvent notification, CancellationToken cancellationToken)
    {
        var article = notification.Value;
        _eventBus.Publish("ArticleService.Article.Created",
            new { Id=article.Id,Title=article.Title,Content=article.Content,AuthorId=article.AuthorId,CategoryId=article.CategoryId,IsVisible=article.IsVisible}
        );

        return Task.CompletedTask;
    }

}
