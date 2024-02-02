using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using OptionService.Domain.Entity;
using OptionService.Domain;
using OptionService.Infrastructure;
using AspNetCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using OptionService.Admin.Api.Controllers.Request;

namespace OptionService.Admin.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    [UnitOfWork(typeof(OptionDbContext))]
    public class OptionController : ControllerBase
    {
        private readonly OptionDbContext _optionDbContext;
        private readonly OptionDomainSer _optionService;
        private readonly IOptionRepo _optionRepo;

        public OptionController(OptionDbContext dbCtx, OptionDomainSer ser, IOptionRepo repo)
        {
            this._optionDbContext = dbCtx;
            this._optionService = ser;
            this._optionRepo = repo;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Option?>> FindById(Guid id)
        {
            Guid userId = Guid.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            var option =  await this._optionRepo.FindByIdAsync(id);
            if (option.Type != (int)Option.OptionType.Public && option.OwnerId != userId)
                return BadRequest($"权限不足");
            return option;
        }

        [HttpGet]
        public async Task<ActionResult<Option?>> FindByName(string name)
        {
            return await _optionRepo.FindByNameAsync(name);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddUserOption(AddReq req)
        {
            Guid? id = Guid.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (id == null || id == Guid.Empty)
                return BadRequest();
            var option = await _optionRepo.FindByNameAsync(req.Name);
            if (option != null)
                return BadRequest($"名称已存在");
            Option.Builder build = new Option.Builder();
            build.Name(req.Name).Value(req.Value).Type((int)Option.OptionType.User).OwnerId(id);
            Option newOpt = build.Build();
            return await _optionRepo.CreateAsync(newOpt);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Guid>> AddPublicOption(AddReq req)
        {
            var option = await _optionRepo.FindByNameAsync(req.Name);
            if (option != null)
                return BadRequest($"名称已经存在");

            Option.Builder builder = new Option.Builder();
            builder.Value(req.Value).Name(req.Name).Type((int)Option.OptionType.Public).OwnerId(null);
            Option newOpt = builder.Build();
            return await _optionRepo.CreateAsync(newOpt);
        }


        [HttpPut]
        public async Task<ActionResult> UpdateUserOption(string name, string value)
        {
            Guid id = Guid.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var option = await this._optionRepo.FindByNameAsync(name);
            if(option == null)
                return NotFound();
            if (option.OwnerId != id)
                return BadRequest();
            option.Edit(value);
            return Ok();
        }

        [Authorize(Roles ="Admin")]
        [HttpPut]
        public async Task<ActionResult> UpdatePublicOption(string name, string value)
        {
            var option = await _optionRepo.FindByNameAsync(name);
            if (option == null)
                return NotFound();
            option.Edit(value);
            return Ok();
        }

        [HttpDelete]
        [Route("id")]
        public async Task<ActionResult> DeleteUserOption(Guid id)
        {
            Guid userId = Guid.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            Option? option = await this._optionRepo.FindByIdAsync(id);
            if(option==null)
                return NotFound();
            if(option.OwnerId != userId)
                return BadRequest();
            option.SoftDelete();
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("id")]
        public async Task<ActionResult> DeletePublicOption(Guid id)
        {
            Option? option = await this._optionRepo.FindByIdAsync(id);
            if (option == null)
                return NotFound();
            option.SoftDelete();
            return Ok();
        }
    }
}
