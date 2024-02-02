using TagService.Domain.Entity;
using MediatR;

namespace TagService.Domain.Event;

public record class TagUpdatedEvent(Tag Value) : INotification;
