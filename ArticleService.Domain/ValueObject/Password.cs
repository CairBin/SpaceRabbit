namespace ArticleService.Domain.ValueObject;

public record class Password(string HashValue,string Salt);
