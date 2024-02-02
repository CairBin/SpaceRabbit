using CategoryService.Domain.Event;
using EventBus;
using MediatR;


namespace CategoryService.Admin.WebApi.EventHandler;


public class CategoryCreatedEventHandler : INotificationHandler<CategoryCreatedEvent>
{
    private readonly IEventBus _eventBus;
    public CategoryCreatedEventHandler(IEventBus eventBus)
    {
        this._eventBus = eventBus;
    }


    public Task Handle(CategoryCreatedEvent notification, CancellationToken cancellationToken)
    {
        var category = notification.Value;
        this._eventBus.Publish(
            "CategoryService.Category.Created",
            category
        );

        return Task.CompletedTask;
    }

}