using LinkService.Domain.Entity;
using MediatR;

namespace LinkService.Domain.Event
{
    public record class LinkCreatedEvent(Link Value) : INotification;
    
}
