using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using CategoryService.Domain;
using CategoryService.Domain.Entity;
using CategoryService.Infrastructure;
using AspNetCore;
using DomainCommons.Models;

using System.ComponentModel.DataAnnotations;

namespace CategoryService.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryDomainSer _service;
        private readonly ICategoryRepo _repo;
        private readonly IMemoryCacheHelper _cacheHelper;

        public CategoryController(CategoryDomainSer service, ICategoryRepo repo, IMemoryCacheHelper cacheHelper)
        {
            this._service = service;
            this._repo = repo;
            this._cacheHelper = cacheHelper;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Category>> FindById([RequiredGuid] Guid id)
        {
            var category = await this._cacheHelper.GetOrCreateAsync(
                $"CategoryController.FindById.{id}",
                async (e) => await _repo.FindByIdAsync(id)
            );
            if (category == null)
                return NotFound();

            return category;
        }

        [HttpGet]
        public async Task<ActionResult<Category[]>> FindAll()
        {
            Task<Category[]> FindData()
            {
                return _repo.GetAllAsync();
            }

            var task = this._cacheHelper.GetOrCreateAsync(
                $"CategoryController.FindAll",
                async (e) => await FindData()
            );

            return await task;
        }

        [HttpGet]
        public async Task<ActionResult<Category>> FindByName(string name)
        {
            var category = await this._cacheHelper.GetOrCreateAsync(
            $"CategoryController.FindByName.{name}",
            async(e) => await _repo.FindByNameAsync(name)
                );

            if (category == null)
                return NotFound();

            return category;
        }

    }
}
