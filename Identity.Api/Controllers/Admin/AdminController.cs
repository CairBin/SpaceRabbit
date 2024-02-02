using IdentityService.Domain;
using IdentityService.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Commons;
using EventBus;
namespace IdentityService.Api.Controllers.Admin
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IEventBus _eventBus;
        private readonly IIdentityRepo _identityRepo;
        private readonly IdUserManager _idUserManager;
        
        public AdminController(IdUserManager userManager, IEventBus eventBus, IIdentityRepo repository)
        {
            this._eventBus = eventBus;
            this._identityRepo = repository;
            this._idUserManager = userManager;
        }

        [HttpGet]
        public Task<UserDTO[]> FindAllUsers()
        {
            return this._idUserManager.Users.Select(u => UserDTO.Create(u)).ToArrayAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<UserDTO> FindByIdAsync(Guid id)
        {
            var user = await _idUserManager.FindByIdAsync(id.ToString());
            return UserDTO.Create(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> AddAdmin(AddAdminRequest req)
        {
            (var result, var user, var password) = await this._identityRepo
            .AddAdminAsync(req.Username, req.Email);
            if (!result.Succeeded)
                return BadRequest(result.Errors.SumErrors());

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAdminUser(Guid id)
        {
            await this._identityRepo.RemoveUserAsync(id);
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateAdmin(Guid id, EditAdminRequest req)
        {
            var user = await this._identityRepo.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("用户没找到");
            }
            user.ChangeEmail(req.Email);
            await this._idUserManager.UpdateAsync(user);
            return Ok();
        }

    }
}
