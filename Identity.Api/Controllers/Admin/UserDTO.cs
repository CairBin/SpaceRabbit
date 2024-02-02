using IdentityService.Domain.Entity;
namespace IdentityService.Api.Controllers.Admin
{
    public record UserDTO(Guid Id, string UserName, string Email, DateTime Created)
    {
        public static UserDTO Create(User user)
        {
            return new UserDTO(user.Id, user.UserName, user.Email, user.Created);
        }
    }
}
