using MediatR;
using EventBus;
using CategoryService.Domain.Event;

namespace CategoryService.Admin.WebApi.EventHandler;
public class CategoryDeletedEventHandler : INotificationHandler<CategoryDeletedEvent>
{
    private readonly IEventBus _eventBus;
    public CategoryDeletedEventHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public Task Handle(CategoryDeletedEvent notification, CancellationToken cancellationToken)
    {
        var id = notification.Id;
        this._eventBus.Publish("CategoryService.Category.Deleted", new { Id = id });
        return Task.CompletedTask;
    }
}
