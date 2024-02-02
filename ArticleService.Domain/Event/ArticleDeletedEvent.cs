using ArticleService.Domain.Entity;
using MediatR;


namespace ArticleService.Domain.Event;
public record class ArticleDeletedEvent(Guid Id): INotification;