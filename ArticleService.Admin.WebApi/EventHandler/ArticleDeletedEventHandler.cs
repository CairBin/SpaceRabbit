using ArticleService.Domain.Event;
using EventBus;
using MediatR;


namespace ArticleService.Admin.WebApi.EventHandler;

public class ArticleDeletedEventHandler: INotificationHandler<ArticleDeletedEvent>
{
    private readonly IEventBus _eventBus;
    public ArticleDeletedEventHandler(IEventBus eventBus)
    {
        this._eventBus = eventBus;
    }

    public Task Handle(ArticleDeletedEvent notification, CancellationToken cancellationToken)
    {
        var id = notification.Id;
        this._eventBus.Publish("ArticleService.Article.Deleted", new { Id = id });

        return Task.CompletedTask;
    }

}
