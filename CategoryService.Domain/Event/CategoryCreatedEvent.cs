using CategoryService.Domain.Entity;
using MediatR;
namespace CategoryService.Domain.Event;
public record class CategoryCreatedEvent(Category Value): INotification;
