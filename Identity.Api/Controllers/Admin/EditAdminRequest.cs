using FluentValidation;

namespace IdentityService.Api.Controllers.Admin;


public record EditAdminRequest(string Email);

public class EditAdminRequestValidator : AbstractValidator<EditAdminRequest> { 
    public EditAdminRequestValidator()
    {
        RuleFor(e => e.Email).NotNull().NotEmpty();
    }
}
