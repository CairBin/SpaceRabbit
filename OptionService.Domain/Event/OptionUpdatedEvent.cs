using MediatR;
using OptionService.Domain.Entity;

namespace OptionService.Domain.Event
{
    public record class OptionUpdatedEvent(Option Value) : INotification;

}
