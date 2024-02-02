using FluentValidation;

namespace IdentityService.Api.Controllers.Login;


public record LoginByUsernameRequest(string Username, string Password);

public class LoginByUsernameRequestValidator : AbstractValidator<LoginByUsernameRequest>
{
    public LoginByUsernameRequestValidator()
    {
        RuleFor(e=>e.Username).NotNull().NotEmpty();
        RuleFor(e=>e.Password).NotNull().NotEmpty();
    }
}
