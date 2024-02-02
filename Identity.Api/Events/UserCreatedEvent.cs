namespace IdentityService.Api.Events
{
    public record UserCreatedEvent(Guid Id, string UserName, string Password, string Email);
}
