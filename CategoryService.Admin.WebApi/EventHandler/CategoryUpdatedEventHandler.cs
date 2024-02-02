using MediatR;
using EventBus;
using CategoryService.Domain.Event;

namespace CategoryService.Admin.WebApi.EventHandler;

public class CategoryUpdatedEventHandler: INotificationHandler<CategoryUpdatedEvent>
{
    private readonly IEventBus _eventBus;

    public CategoryUpdatedEventHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public Task Handle(CategoryUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var category = notification.Value;
        this._eventBus.Publish(
            "CategoryService.Category.Updated",
            category
        );

        return Task.CompletedTask;
    }

}

