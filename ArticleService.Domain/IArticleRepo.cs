using ArticleService.Domain.Entity;
using ArticleService.Domain.ValueObject;

namespace ArticleService.Domain;
public interface IArticleRepo
{
    public Task<Guid> CreateAsync(Article article);

    public Task<Article?> FindByIdAsync(Guid id);

    public Task<Article[]> FindByAuthorIdAsync(Guid authorId);

    public Task<Article[]> FindByCategoryAsync(Guid categoryId);


    public Task<Article[]> GetAllAsync();

    //public Task<Article> EditAsync(Guid articleId, string title, string content);

    //public Task SoftDeleteAsync(Guid articleId);
    //public Task<Article> ChangeCategoryAsync(Guid articleId, Guid? categoryId);
    //public Task AddViewNumberAsync(Guid articleId);

    //public Task SetPasswordAsync(Guid articleId,string password);
    //public Task CancelPasswordAsync(Guid articleId);

    //public Task<bool> CheckPasswordAsync(Guid articleId,string password);

}