using MediatR;
using OptionService.Domain.Entity;

namespace OptionService.Domain.Event
{
    public record class OptionCreatedEvent(Option Value) : INotification;

}
