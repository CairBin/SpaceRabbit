using CategoryService.Domain.Entity;
using MediatR;

namespace CategoryService.Domain.Event;

public record class CategoryUpdatedEvent(Category Value): INotification;