using MediatR;
using CommentService.Domain.Entity;

namespace CommentService.Domain.Event
{
    public record class CommentCreatedEvent(Comment Value) : INotification;

}
