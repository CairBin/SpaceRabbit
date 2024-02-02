using MediatR;
using CommentService.Domain.Entity;

namespace CommentService.Domain.Event
{
    public record class CommentUpdatedEvent(Comment Value) : INotification;

}
