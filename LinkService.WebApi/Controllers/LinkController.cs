using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LinkService.Domain;
using LinkService.Domain.Entity;
using LinkService.Infrastructure;
using AspNetCore;

namespace LinkService.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LinkController : ControllerBase
    {
        private readonly ILinkRepo _linkRepo;
        private readonly LinkDbContext _context;
        private readonly IDistributedCacheHelper _distributedCacheHelper;

        public LinkController(ILinkRepo repo, LinkDbContext dbCtx, IDistributedCacheHelper cache)
        {
            this._linkRepo = repo;
            this._context = dbCtx;  
            this._distributedCacheHelper = cache;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Link?>> FindById(Guid id)
        {
            return await _distributedCacheHelper.GetOrCreateAsync($"LinkController.FindById.{id}", async (e) => await _linkRepo.FindByIdAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult<Link[]>> GetAll()
        {
            return await _distributedCacheHelper.GetOrCreateAsync($"LinkController.GetAll", async (e) => await _linkRepo.GetAllAsync());
        }


    }
}
