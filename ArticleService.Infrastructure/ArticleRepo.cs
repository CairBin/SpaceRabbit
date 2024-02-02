using ArticleService.Domain;
using ArticleService.Domain.Entity;
using ArticleService.Domain.ValueObject;
using Microsoft.EntityFrameworkCore;

namespace ArticleService.Infrastructure
{
    public class ArticleRepo : IArticleRepo
    {
        private readonly ArticleDbContext _dbCtx;

        public ArticleRepo(ArticleDbContext dbCtx)
        {
            this._dbCtx = dbCtx;
        }


        public async Task<Guid> CreateAsync(Article article)
        {
            await this._dbCtx.Articles.AddAsync(article);
            return article.Id;
        }

        public async Task<Article[]> FindByAuthorIdAsync(Guid authorId)
        {
            return await this._dbCtx.Articles.Where(x => x.AuthorId == authorId).ToArrayAsync();

        }

        public async Task<Article[]> FindByCategoryAsync(Guid categoryId)
        {
            return await this._dbCtx.Articles
                .Where(x => x.CategoryId == categoryId).ToArrayAsync();
        }

        public async Task<Article?> FindByIdAsync(Guid id)
        {
            return await this._dbCtx.Articles.FindAsync(id);
        }

        public async Task<Article[]> GetAllAsync()
        {
            return await this._dbCtx.Articles.ToArrayAsync();
        }
    }
}
