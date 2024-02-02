using ArticleService.Domain.Event;
using EventBus;
using MediatR;


namespace ArticleService.Admin.WebApi.EventHandler;

public class ArticleUpdatedEventHandler : INotificationHandler<ArticleUpdatedEvent>
{
    private readonly IEventBus _eventBus;
    public ArticleUpdatedEventHandler(IEventBus eventBus)
    {
        this._eventBus = eventBus;
    }

    public Task Handle(ArticleUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var article = notification.Value;
        if(article.IsVisible == true)
        {
            _eventBus.Publish("ArticleService.Article.Updated",
                new
                {
                    Id = article.Id,
                    Title = article.Title,
                    Content = article.Content,
                    AuthorId = article.AuthorId,
                    CategoryId = article.CategoryId,
                    IsVisible = article.IsVisible
                }
            );
        }
        else
        {
            _eventBus.Publish("ArticleService.Article.Updated", new { Id = article.Id });
        }

        return Task.CompletedTask;
    }

}
