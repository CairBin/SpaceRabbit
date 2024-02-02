using TagService.Domain.Entity;
using MediatR;

namespace TagService.Domain.Event;

public record class TagCreatedEvent(Tag Value) : INotification;
