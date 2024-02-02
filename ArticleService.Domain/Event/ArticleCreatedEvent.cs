using ArticleService.Domain.Entity;
using MediatR;

namespace ArticleService.Domain.Event;
public record class ArticleCreatedEvent(Article Value) : INotification;