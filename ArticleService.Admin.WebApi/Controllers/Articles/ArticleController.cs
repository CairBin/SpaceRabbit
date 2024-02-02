using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspNetCore;
using ArticleService.Domain;
using ArticleService.Infrastructure;
using ArticleService.Domain.Entity;
using System.ComponentModel.DataAnnotations;
using ArticleService.Admin.WebApi.Controllers.Articles.Request;
using System.Security.Claims;
namespace ArticleService.Admin.WebApi.Controllers.Articles
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    [UnitOfWork(typeof(ArticleDbContext))]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleDbContext _dbCtx;
        private readonly ArticleDomainSer _service;
        private readonly ArticleRepo _repository;

        public ArticleController(ArticleDbContext dbCxt, ArticleDomainSer service, ArticleRepo repository)
        {
            this._dbCtx = dbCxt;
            this._service = service;
            this._repository = repository;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Article?>> FindById([Required]Guid id)
        {
            return await _repository.FindByIdAsync(id);
        }

        [HttpGet]
        [Route("{categoryId}")]
        public async Task<ActionResult<Article[]>> FindByCategoryId([RequiredGuid]Guid categoryId)
        {
            return await _repository.FindByCategoryAsync(categoryId);
        }

        [HttpGet]
        [Route("{authorId}")]
        public async Task<ActionResult<Article[]>> FindByAuthorId([Required]Guid authorId)
        {
            return await _repository.FindByAuthorIdAsync(authorId);
        }


        [HttpPost]
        public async Task<ActionResult<Guid>> Add(ArticleAddRequest req)
        {
            Guid authorId = Guid.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            Guid articleId = await this._service.CreateAsync(
                req.Title, 
                req.Content, 
                authorId,
                req.CategoryId,
                req.password,
                req.publicField,
                req.privateField,
                req.Created,
                req.CoverUrl);
            return articleId;
        }
        
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> Update([Required]Guid id,ArticleUpdateRequest req)
        {
            var article = await this._repository.FindByIdAsync(id);
            if(article == null)
                return NotFound("ID不存在");
            article.EditArticle(req.Title, req.Content, new Domain.ValueObject.AdditionalField(req.PublicField, req.PrivateField));
            article.SetCreated(req.Created);
            article.SetCover(req.CoverUrl);
            if (req.IsVisible == true)
                article.Show();
            else
                article.Hide();

            if (req.Password == null)
                article.CancelPassword();
            else
            {
                await this._service.SetPasswordAsync(id, req.Password);
            }

            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> SetVisible([RequiredGuid]Guid id,bool isVisible)
        {
            var article = await this._repository.FindByIdAsync(id);
            if (article == null)
                return NotFound("ID不存在");
            if (isVisible == true)
                article.Show();
            else
                article.Hide();

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete([Required]Guid id)
        {
            bool flag = await this._service.DeleteAsync(id);
            if (!flag)
                return BadRequest("删除失败，可能文章不存在");

            return Ok();
        }

    }
}
