using FluentValidation;
namespace IdentityService.Api.Controllers.Login;


public record LoginByEmailRequest(string Email,string Password);

public class LoginByEmailRequestValidator : AbstractValidator<LoginByEmailRequest>
{
    public LoginByEmailRequestValidator()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty();
        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}

