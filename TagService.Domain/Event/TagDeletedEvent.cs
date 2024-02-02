using TagService.Domain.Entity;
using MediatR;

namespace TagService.Domain.Event;

public record class TagDeletedEvent(Guid id) : INotification;
