using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AspNetCore;
using PageService.Domain.Entity;
using PageService.Domain.ValueObject;
using PageService.Domain;
using PageService.Infrastructure;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;


namespace PageService.Admin.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    [UnitOfWork(typeof(PageDbContext))]
    public class PageController : ControllerBase
    {
        private readonly PageDbContext _pageDbContext;
        private readonly PageDomainSer _pageDomainSer;
        private readonly IPageRepo _pageRepo;

        public PageController(PageDbContext pageDtx, PageDomainSer service, IPageRepo pageRepo)
        {
            _pageDbContext = pageDtx;
            _pageDomainSer = service;
            _pageRepo = pageRepo;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Page?>> FindById([RequiredGuid]Guid id)
        {
            return await _pageRepo.FindByIdAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<Page[]>> GetAll()
        {
            return await _pageRepo.GetAllAsync();
        }

        [HttpGet]
        public async Task<ActionResult<Page?>> FindByName([Required]string name)
        {
            return await _pageRepo.FindByNameAsync(name);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Add(string title, string content, string pageName, string publicField, string privateField)
        {
            try
            {
                Guid id = await this._pageDomainSer.CreateAsync(title, content, pageName, new AdditionalField(publicField, privateField));
                return id;
            }catch (Exception ex)
            {
                return BadRequest("添加失败，可能由于页面名称重复");
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Update([RequiredGuid]Guid id, string title, string content, string publicField, string privateField)
        {
            var page = await _pageDbContext.Pages.FindAsync(id);
            if (page == null)
                return NotFound();

            page.EditPage(title, content, new AdditionalField(publicField, privateField));
            return Ok();
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete([RequiredGuid]Guid id)
        {
            bool flag = await _pageDomainSer.DeleteAsync(id);
            if (!flag)
                return NotFound();

            return Ok();
        }


    }
}
