using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using LinkService.Domain.Entity;
using LinkService.Domain;
using LinkService.Infrastructure;
using LinkService.Admin.Api.Controllers.Request;
using AspNetCore;

namespace LinkService.Admin.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    [UnitOfWork(typeof(LinkDbContext))]
    public class LinkController : ControllerBase
    {
        private readonly LinkDbContext _dbCtx;
        private readonly ILinkRepo _linkRepo;

        public LinkController(LinkDbContext dbCtx, ILinkRepo repo)
        {
            this._dbCtx = dbCtx;
            this._linkRepo = repo;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Link?>> FindById(Guid id)
        {
            return await this._linkRepo.FindByIdAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<Link[]>> GetAll()
        {
            return await this._linkRepo.GetAllAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Add(AddReq req)
        {
            Link link = new Link(req.Title,req.Description,req.LogoUrl,req.SiteUrl);
            return await this._linkRepo.CreateAsync(link);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Guid>> Update(Guid id,UpdateReq req)
        {
            Link? link = await this._linkRepo.FindByIdAsync(id);
            if (link == null)
                return BadRequest();
            link.EditLink(req.Title, req.Description, req.LogoUrl, req.SiteUrl);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            Link? link = await _linkRepo.FindByIdAsync(id);
            if (link == null)
                return BadRequest();
            return Ok();
        }

    }
}
