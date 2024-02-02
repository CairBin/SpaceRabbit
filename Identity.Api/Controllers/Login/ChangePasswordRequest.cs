using FluentValidation;

namespace IdentityService.Api.Controllers.Login;

public record ChangePasswordRequest(string Password, string Password2);

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(e => e.Password).NotNull().NotEmpty().Equal(e => e.Password2);
        RuleFor(e => e.Password2).NotNull().NotEmpty();
    }
}

