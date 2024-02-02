using IdentityService.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Jwt;
using IdentityService.Domain.Entity;

namespace IdentityService.Infrastructure
{
    public class IdentityDomainSer
    {
        private readonly IIdentityRepo _iIdentityRepo;
        private readonly ITokenService _iTokenService;
        private readonly IOptions<JWTOptions> _iOptJwt;

        public IdentityDomainSer(IIdentityRepo iIdentityRepo,ITokenService iTokenService,IOptions<JWTOptions> iOptJwt)
        {
            this._iIdentityRepo = iIdentityRepo;
            this._iTokenService = iTokenService;
            this._iOptJwt = iOptJwt;
        }

        private async Task<SignInResult> CheckUsernameAndPwd(string username,string password)
        {
            var user = await this._iIdentityRepo.FindByUsernameAsync(username);
            if (user == null)
                return SignInResult.Failed;

            var result = await this._iIdentityRepo.CheckSignInAsync(user, password, true);

            return result;
        }

        private async Task<SignInResult> CheckEmailAndPwd(string email, string password)
        {
            var user = await this._iIdentityRepo.FindByEmailAsync(email);
            if (user == null)
                return SignInResult.Failed;

            var result = await this._iIdentityRepo.CheckSignInAsync(user, password, true);

            return result;
        }

        private async Task<String> BuildTokenAsync(User user)
        {
            var roles = await this._iIdentityRepo.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), user.Email));
            foreach(string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            return this._iTokenService.BuildToken(claims, _iOptJwt.Value);
        }

        public async Task<(SignInResult Result, string? Token)> LoginByUsername(string username,string password)
        {
            var checkRes = await CheckUsernameAndPwd(username, password);
            if(checkRes.Succeeded)
            {
                var user = await _iIdentityRepo.FindByUsernameAsync(username);
                string token = await BuildTokenAsync(user);
                return (SignInResult.Success, token);
            }

            return (checkRes, null);

        }

        public async Task<(SignInResult Result, string? Token)> LoginByEmail(string email,string password)
        {
            var checkRes = await CheckEmailAndPwd(email, password);
            if (checkRes.Succeeded)
            {
                var user = await _iIdentityRepo.FindByEmailAsync(email);
                string token = await BuildTokenAsync(user);
                return (SignInResult.Success, token);
            }

            return (checkRes, null);
        }



    }
}
