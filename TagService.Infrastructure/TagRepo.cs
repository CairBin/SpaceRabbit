using TagService.Domain;
using TagService.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace TagService.Infrastructure
{
    public class TagRepo : ITagRepo
    {
        private readonly TagDbContext _dbCtx;
        public TagRepo(TagDbContext dbCtx)
        {
            this._dbCtx = dbCtx;
        }

        public async Task<Guid> CreateAsync(Tag tag)
        {
            await this._dbCtx.AddAsync(tag);
            return tag.Id;
        }


        public async Task<Tag?> FindByIdAsync(Guid id)
        {
            var tag = await this._dbCtx.Tags.FindAsync(id);
            return tag;
        }




        public async Task<Tag[]> FindByArticleId(Guid articleId)
        {
            return await this._dbCtx.Tags.Where(x => x.ArticleId == articleId).ToArrayAsync();
        }


    }
}
