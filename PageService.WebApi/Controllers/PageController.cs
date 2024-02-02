using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PageService.Domain.Entity;
using PageService.Domain;
using PageService.Infrastructure;
using AspNetCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PageService.WebApi.Controllers.ViewModel;

namespace PageService.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        private readonly PageDomainSer _service;
        private readonly IPageRepo _repo;
        private readonly IMemoryCacheHelper _cacheHelper;
        private readonly IDistributedCacheHelper _distributedCacheHelper;

        public PageController(PageDomainSer service, IPageRepo repo, IMemoryCacheHelper cacheHelper, IDistributedCacheHelper distributedCacheHelper)
        {
            this._service = service;
            this._repo = repo;
            this._cacheHelper = cacheHelper;
            this._distributedCacheHelper = distributedCacheHelper;
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<PageVm?>> FindById([RequiredGuid]Guid id)
        {
            var page = await _distributedCacheHelper.GetOrCreateAsync(
                $"PageController.FindById.{id}",
                async (e) => await _repo.FindByIdAsync(id)
                );
            if (page == null)
                return NotFound();

            return Ok(PageVm.Create(page));
        }

        [HttpGet]
        public async Task<ActionResult<PageVm[]>> GetAll()
        {
            Task<Page[]> FindData()
            {
                return _repo.GetAllAsync();
            }

            var task = this._distributedCacheHelper.GetOrCreateAsync($"PageController.GetAll", async (e) => PageVm.Create(await FindData()));

            return await task;
        }

        [HttpGet]
        public async Task<ActionResult<PageVm>> FindByName([Required]string name)
        {
            var page = await this._distributedCacheHelper.GetOrCreateAsync($"PageController.FindByName.{name}",
                async (e) => await _repo.FindByNameAsync(name));
            if (page == null)
                return NotFound();

            return PageVm.Create(page);
        }

    }
}
