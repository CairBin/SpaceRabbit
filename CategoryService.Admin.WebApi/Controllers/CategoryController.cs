using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using AspNetCore;
using CategoryService.Domain.Entity;
using CategoryService.Domain;
using CategoryService.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace CategoryService.Admin.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    [UnitOfWork(typeof(CategoryDbContext))]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryDbContext _dbContext;
        private readonly CategoryDomainSer _service;
        private readonly ICategoryRepo _repo;

        public CategoryController(CategoryDbContext dbContext, CategoryDomainSer service, ICategoryRepo repo)
        {
            this._dbContext = dbContext;
            this._repo = repo;
            this._service = service;
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Category?>> FindById([Required]Guid id)
        {
            return await _repo.FindByIdAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<Category[]>> GetAll()
        {
            return await _repo.GetAllAsync();
        }

        [HttpGet]
        public async Task<ActionResult<Category?>> FindByName([Required]string name)
        {
            return await _repo.FindByNameAsync(name);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Add([Required]string name)
        {
            try
            {
                var id = await _service.CreateAsync(name);
                return Ok(id);
            }catch (Exception ex)
            {
                return BadRequest($"添加失败，可能分类名称已经存在");
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Update([Required]Guid id, [Required]string name)
        {
            var nameCate = await _repo.FindByNameAsync(name);
            if (nameCate != null)
                return BadRequest("分类名称重复: " + name);

            var category = await _repo.FindByIdAsync(id);
            if(category == null)
                return NotFound();

            
            category.ChangeName(name);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete([Required]Guid id)
        {
            bool flag = await _service.DeleteAsync(id);
            if (!flag)
                return BadRequest("删除失败");

            return Ok();
        }

    }
}
