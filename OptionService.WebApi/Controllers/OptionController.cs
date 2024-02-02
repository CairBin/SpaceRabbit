using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using OptionService.Domain;
using OptionService.Domain.Entity;
using OptionService.Infrastructure;
using AspNetCore;

namespace OptionService.WebApi.Controllers
{

    /// <summary>
    /// 此控制器仅用于对公开Option（即OwnerId = null && Type = 0）进行查找
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        private readonly IOptionRepo _optionRepo;
        private readonly OptionDbContext _optionDbContext;
        private readonly OptionDomainSer _optionDomainSer;
        private readonly IDistributedCacheHelper _distributedCacheHelper;

        public OptionController(IOptionRepo repo, OptionDbContext dbCtx, OptionDomainSer ser, IDistributedCacheHelper cache)
        {
            this._optionDbContext = dbCtx;
            this._optionDomainSer = ser;
            this._distributedCacheHelper = cache;
            this._optionRepo = repo;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Option>> FindById(Guid id)
        {
            var option = await _distributedCacheHelper.GetOrCreateAsync($"OptionController.FindById.{id}",async(e)=> await _optionRepo.FindByIdAsync(id));
            if (option == null)
                return NotFound();

            if (option.Type != (int)Option.OptionType.Public)
                return BadRequest($"权限不足");

            return option;
        }

        [HttpGet]
        public async Task<ActionResult<Option>> FindByName(string name)
        {
            var option = await _distributedCacheHelper.GetOrCreateAsync($"OptionController.FindByName.{name}", async (e) => await _optionRepo.FindByNameAsync(name));
            if (option == null)
                return NotFound();
            if (option.Type != (int)Option.OptionType.Public)
                return BadRequest("权限不足");
        
            return option;
        }

        

    }
}
