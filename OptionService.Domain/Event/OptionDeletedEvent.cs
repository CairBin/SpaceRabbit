using MediatR;
using OptionService.Domain.Entity;

namespace OptionService.Domain.Event
{
    public record class OptionDeletedEvent(Guid Id) : INotification;

}
