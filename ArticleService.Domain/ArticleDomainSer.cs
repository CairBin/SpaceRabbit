using ArticleService.Domain.Entity;
using System.Threading.Tasks;
using DomainCommons.Models;
using ArticleService.Domain.ValueObject;

namespace ArticleService.Domain;
public class ArticleDomainSer
{
    private readonly IArticleRepo _repository;
    public ArticleDomainSer(IArticleRepo repository)
    {
        this._repository = repository;
    }

    public async Task<Article?> FindByIdAndAddViewNumberAsync(Guid id)
    {
        var article = await _repository.FindByIdAsync(id);
        if (article == null)
            return null;
        article.AddViewNumber();
        return article;
    }

    public async Task<bool> CheckArticlePasswordAsync(Guid articleId,string? password)
    {
        var article = await this._repository.FindByIdAsync(articleId);
        if (article == null)
            return false;
        if (string.IsNullOrEmpty(password) && article.HashPassword == null)
            return true;
        if (!string.IsNullOrEmpty(password) && article.HashPassword == null)
            return false;
        if (string.IsNullOrEmpty(password) && article.HashPassword != null)
            return false;


        if (article.HashPassword.HashValue == Commons.HashHelper.ComputeMd5(password + article.HashPassword.Salt))
            return true;

        return false;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var article = await this._repository.FindByIdAsync(id);
        if (article == null)
            return false;
        article.SoftDelete();
        return true;
    }

    public async Task ChangeCategoryAsync(Guid articleId, Guid? categoryId)
    {
        var article = await this._repository.FindByIdAsync(articleId);
        if (article == null)
            throw new Exception("文章不存在");
        article.ChangeType(categoryId);
    }

    public Task<Guid> CreateAsync(string title, string content, Guid authorId, Guid? categoryId, string? password, string publicField, string privateField,DateTime created, Uri? coverUrl)
    {
        AdditionalField field = new AdditionalField(publicField, privateField);
        if (string.IsNullOrEmpty(password))
        {
            Article newArticle = new Article(title, content, authorId,categoryId, null, field,created,coverUrl);
            return this._repository.CreateAsync(newArticle);
        }


        string salt = Commons.HashHelper.GenerateSalt();
        Password hashPwd = new Password(Commons.HashHelper.ComputeMd5(password + salt), salt);
        Article article = new Article(title, content, authorId, categoryId, hashPwd, field,created,coverUrl);
        return this._repository.CreateAsync(article);
    }

    public async Task<bool> SetPasswordAsync(Guid id, string password)
    {
        var article = await this._repository.FindByIdAsync(id);
        if (article == null)
            return false;
        string salt = Commons.HashHelper.GenerateSalt();
        Password hashPwd = new Password(Commons.HashHelper.ComputeMd5(password + salt), salt);
        article.SetSecret(hashPwd);
        return true;
    }

    public async Task<bool> CancelPasswordAsync(Guid id)
    {
        var article = await this._repository.FindByIdAsync(id);
        if (article == null)
            return false;
        article.CancelPassword();
        return true;
    }


}