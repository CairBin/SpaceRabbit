using EventBus;
using ArticleService.Domain;
using ArticleService.Infrastructure;

namespace ArticleService.Admin.WebApi.EventHandler
{

    [EventName("CategoryService.Category.Deleted")]
    public class CategoryDeletedEventHandler : DynamicIntegrationEventHandler
    {
        private readonly IArticleRepo _articleRepo;
        private readonly ArticleDbContext _dbContext;
        private readonly ArticleDomainSer _ser;

        public CategoryDeletedEventHandler(ArticleDbContext dbContext, IArticleRepo articleRepo, ArticleDomainSer ser)
        {
            this._dbContext = dbContext;
            this._articleRepo = articleRepo;
            this._ser = ser;
        }

        public override async Task HandleDynamic(string eventName, dynamic eventData)
        {
            Guid id = Guid.Parse(eventData.Id);
            var articles = await this._articleRepo.FindByCategoryAsync(id);
            foreach(var article in articles)
            {
                await _ser.ChangeCategoryAsync(article.Id, null);
            }

        }
    }
}
