

namespace CommentService.Domain;

public interface IRecaptchaValidator
{
    public Task<bool> ValidateAsync(string token);
}
