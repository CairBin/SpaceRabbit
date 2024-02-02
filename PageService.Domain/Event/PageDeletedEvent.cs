using MediatR;
using PageService.Domain.Entity;

namespace PageService.Domain.Event;

public record class PageDeletedEvent(Guid id) : INotification;
