using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ArticleService.Domain;
using ArticleService.Infrastructure;
using ArticleService.Domain.Entity;
using DomainCommons.Models;
using AspNetCore;
using ArticleService.WebApi.Controllers.Articles.ViewModel;

using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ArticleService.WebApi.Controllers.Articles
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleDomainSer _service;
        private readonly IArticleRepo _repository;
        private readonly IMemoryCacheHelper _cacheHelper;
        private readonly IDistributedCacheHelper _distributedCacheHelper;   //Redis分布式缓存
        //管理员的接口不应该与此处的复用，因为管理员接口没有缓存机制
        public ArticleController(ArticleDomainSer service, IArticleRepo repository,IMemoryCacheHelper cacheHelper, IDistributedCacheHelper distributedCacheHelper)
        {
            this._service = service;
            this._repository = repository;
            this._cacheHelper = cacheHelper;
            this._distributedCacheHelper = distributedCacheHelper;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ArticleVm>> FindById([RequiredGuid] Guid id)
        {
            var article = await this._distributedCacheHelper.GetOrCreateAsync(
                $"ArticleController.FindById.{id}",
                async (e) => ArticleVm.Create(await _service.FindByIdAndAddViewNumberAsync(id))
            ) ;
            if(article == null)
                return NotFound();
            
            return article;
        }

        [HttpGet]
        public async Task<ActionResult<ArticleVm[]>> FindAll()
        {
            Task<Article[]> FindData()
            {
                return _repository.GetAllAsync();
            }

            var task = this._distributedCacheHelper.GetOrCreateAsync(
                $"ArticleController.FindAll",
                async (e) =>ArticleVm.Create(await FindData())
            );

            return await task;
        }

        [HttpGet]
        [Route("{authorId}")]
        public async Task<ActionResult<ArticleVm[]>> FindByAuthorId([RequiredGuid]Guid authorId)
        {
            Task<Article[]> FindData()
            {
                return _repository.FindByAuthorIdAsync(authorId);
            }

            var task = this._distributedCacheHelper.GetOrCreateAsync(
                    $"ArticleController.FindByAuthorId",
                    async(e)=>ArticleVm.Create(await FindData())
                );

            return await task;
        }

        [HttpGet]
        [Route("{categoryId}")]
        public async Task<ActionResult<ArticleVm[]>> FindByCategoryId([RequiredGuid]Guid categoryId)
        {
            Task<Article[]> FindData()
            {
                return _repository.FindByCategoryAsync(categoryId);
            }
            var task = this._distributedCacheHelper.GetOrCreateAsync($"ArticleController.FindByCategoryId",
                    async(e)=>ArticleVm.Create(await FindData())
                );
            return await task;
        }

        [HttpGet]
        [Route("{secretId}")]
        public async Task<ActionResult<ArticleVm>> FindSecretById([RequiredGuid]Guid id,[Required]string password)
        {
            async Task<Article?> FindAndCheckData()
            {
                var article = await _service.FindByIdAndAddViewNumberAsync(id);
                if (await _service.CheckArticlePasswordAsync(id, password))
                    return article;
                else
                    return null;
            }
            var article = await this._distributedCacheHelper.GetOrCreateAsync(
                $"ArticleController.FindSecretById.{id}",
                async (e) => ArticleVm.CreateSecret(await FindAndCheckData())
            );
            if (article == null)
                return NotFound();
            return article;
        }


    }
}
