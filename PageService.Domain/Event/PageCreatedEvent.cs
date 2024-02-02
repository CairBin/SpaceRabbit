using PageService.Domain.Entity;
using MediatR;

namespace PageService.Domain.Event;

public record class PageCreatedEvent(Page Value) : INotification;

