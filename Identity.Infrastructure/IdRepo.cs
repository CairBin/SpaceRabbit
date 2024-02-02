using IdentityService.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using IdentityService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text;
using Commons;

namespace IdentityService.Infrastructure
{
    class IdRepo: IIdentityRepo
    {
        private readonly ILogger<IdRepo> _iLogger;
        private readonly RoleManager<Role> _roleManager;
        private readonly IdUserManager _userManager;

        public IdRepo(IdUserManager userManager, RoleManager<Role> roleManager, ILogger<IdRepo> iLogger)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._iLogger = iLogger;
        }

        private static IdentityResult ErrorResult(string msg)
        {
            IdentityError idError = new IdentityError { Description = msg };
            return IdentityResult.Failed(idError);
        }

        private string GeneratePassword()
        {
            var options = _userManager.Options.Password;
            int length = options.RequiredLength;
            bool nonAlphanumeric = options.RequireNonAlphanumeric;
            bool digit = options.RequireDigit;
            bool lowercase = options.RequireLowercase;
            bool uppercase = options.RequireUppercase;
            StringBuilder password = new StringBuilder();
            Random random = new Random();
            while (password.Length < length)
            {
                char c = (char)random.Next(32, 126);
                password.Append(c);
                if (char.IsDigit(c))
                    digit = false;
                else if (char.IsLower(c))
                    lowercase = false;
                else if (char.IsUpper(c))
                    uppercase = false;
                else if (!char.IsLetterOrDigit(c))
                    nonAlphanumeric = false;
            }

            if (nonAlphanumeric)
                password.Append((char)random.Next(33, 48));
            if (digit)
                password.Append((char)random.Next(48, 58));
            if (lowercase)
                password.Append((char)random.Next(97, 123));
            if (uppercase)
                password.Append((char)random.Next(65, 91));
            return password.ToString();
        }

        public Task<IdentityResult> AccessFailedAsync(User user)
        {
            return this._userManager.AccessFailedAsync(user);
        }

        public async Task<(IdentityResult, User?, string password)> AddAdminAsync(string username,string email)
        {
            if(await this.FindByUsernameAsync(username)!=null)
                return (ErrorResult($"已经存在用户名{username}"), null, null);

            if(await this.FindByEmailAsync(email)!=null)
                return (ErrorResult($"已经存在Email {email}"), null, null);

            User user = new User(username, email);
            string password = GeneratePassword();
            var result = await CreateAsync(user, password);
            if(!result.Succeeded)
                return (result,null,null);
            result = await AddToRoleAsync(user, "Admin");
            if (!result.Succeeded)
                return (result, null, null);

            return (IdentityResult.Success, user, password);
        }

        public async Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            if (!await this._roleManager.RoleExistsAsync(role))
            {
                Role roleName = new Role { Name = role };
                var result = await this._roleManager.CreateAsync(roleName);
                if (result.Succeeded == false)
                {
                    return result;
                }
            }
            return await this._userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> ChangePasswordAsync(Guid userId, string password)
        {
            if (password.Length < 6)
            {
                IdentityError err = new IdentityError();
                err.Code = "Password Invalid";
                err.Description = "密码长度不能少于6";
                return IdentityResult.Failed(err);
            }
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetPwdResult = await _userManager.ResetPasswordAsync(user, token, password);
            return resetPwdResult;
        }

        public async Task<SignInResult> CheckSignInAsync(User user, string password, bool lockoutOnFailure)
        {
            if (await this._userManager.IsLockedOutAsync(user))
                return SignInResult.LockedOut;

            var success = await this._userManager.CheckPasswordAsync(user, password);
            if(success)
                return SignInResult.Success;


            if (lockoutOnFailure)
            {
                var r = await AccessFailedAsync(user);
                if (!r.Succeeded)
                    throw new ApplicationException("AccessFailed failed");
            }


            return SignInResult.Failed;
        }

        public Task<IdentityResult> CreateAsync(User username, string password)
        {
            return this._userManager.CreateAsync(username, password);
        }

        public Task<User?> FindByEmailAsync(string email)
        {
            return this._userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task<User?> FindByIdAsync(Guid userId)
        {
            return this._userManager.FindByIdAsync(userId.ToString());
        }

        public Task<User?> FindByUsernameAsync(string username)
        {
            return this._userManager.FindByNameAsync(username.ToString());
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            return this._userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> RemoveUserAsync(Guid id)
        {
            var user = await FindByIdAsync(id);
            var userLoginStore = this._userManager.UserLoginStore;
            var noneCT = default(CancellationToken);
            //而且要先删除aspnetuserlogins数据，再软删除User
            var logins = await userLoginStore.GetLoginsAsync(user, noneCT);
            foreach(var login in logins)
            {
                await userLoginStore.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey, noneCT);

            }

            user.SoftDelete();
            var result = await this._userManager.UpdateAsync(user);
            return result;

        }

        public async Task UpdateEmail(Guid id, string email)
        {
            var user = await this._userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                throw new ArgumentException($"用户找不到，id={id}", nameof(id));
            user.ChangeEmail(email);
            await this._userManager.UpdateAsync(user);
        }
    }
}
