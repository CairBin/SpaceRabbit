using CommentService.Domain;
using reCAPTCHA.AspNetCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CommentService.Infrastructure.Options;

namespace CommentService.Infrastructure.Service;

public class RecaptchaValidator : IRecaptchaValidator
{
    private readonly IRecaptchaService _recaptchaService;
    private readonly ILogger<RecaptchaValidator> _logger;
    private readonly IOptionsSnapshot<RecaptchaOptions> _optionsSnapshot;

    public RecaptchaValidator(IRecaptchaService _recaptchaService, ILogger<RecaptchaValidator> logger, IOptionsSnapshot<RecaptchaOptions> optionsSnapshot)
    {
        this._recaptchaService = _recaptchaService;
        this._logger = logger;
        this._optionsSnapshot = optionsSnapshot;
    }

    public async Task<bool> ValidateAsync(string token)
    {
        _logger.LogInformation("Sending token to Google Recaptcha...");

        var result = await _recaptchaService.Validate(token);
        if(!result.success || result.score < 0.5)
        {
            _logger.LogInformation("Google Recaptcha verification failed");
            return false;
        }
        return true;
    }
}
