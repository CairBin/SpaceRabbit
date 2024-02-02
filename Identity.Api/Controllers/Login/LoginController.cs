using IdentityService.Domain;
using IdentityService.Domain.Entity;
using IdentityService.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using Commons;

namespace IdentityService.Api.Controllers.Login;
[Route("[controller]/[action]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IIdentityRepo _iIdRepo;
    private readonly IdentityDomainSer _idDomainSer;

    public LoginController(IdentityDomainSer idDomainSer,IIdentityRepo iIdRepo)
    {
        this._iIdRepo = iIdRepo;
        this._idDomainSer = idDomainSer;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> HelloWorld()
    {
        if (await _iIdRepo.FindByUsernameAsync("admin") != null)
        {
            return StatusCode((int)HttpStatusCode.Conflict, "已经初始化过了");
        }
        User user = new User("admin");
        await _iIdRepo.CreateAsync(user, "123456");
        await _iIdRepo.AddToRoleAsync(user, "Admin");
        //Debug.Assert(r.Succeeded);
        return Ok();
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<UserResponse>> GetMyInfo()
    {
        string myId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await this._iIdRepo.FindByIdAsync(Guid.Parse(myId));
        if (user == null)
            return NotFound();

        return new UserResponse(user.Id, user.Email, user.Created);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<string>> LoginByUsername(LoginByUsernameRequest req)
    {
        (var checkResult, var token) = await this._idDomainSer.LoginByUsername(req.Username,req.Password);
        if (checkResult.Succeeded) return token!;
        else if(checkResult.IsLockedOut)
            return StatusCode((int)HttpStatusCode.Locked,"用户被锁定");
        else
        {
            string msg = checkResult.ToString();
            return BadRequest("登陆失败" + msg);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> ChangeMyPassword(ChangePasswordRequest req)
    {
        Guid userId = Guid.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var resetPwdResult = await this._iIdRepo.ChangePasswordAsync(userId, req.Password);
        if (resetPwdResult.Succeeded)
            return Ok();
        else
            return BadRequest(resetPwdResult.ToString());
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<string>> LoginByEmail(LoginByEmailRequest req)
    {
        (var checkResult,var token) = await this._idDomainSer.LoginByEmail(req.Email,req.Password);
        if (checkResult.Succeeded) return token!;
        else if(checkResult.IsLockedOut) return StatusCode((int)HttpStatusCode.Locked, "用户被锁定");
        else
        {
            string msg = checkResult.ToString();
            return BadRequest("登陆失败" + msg);
        }
    }


}