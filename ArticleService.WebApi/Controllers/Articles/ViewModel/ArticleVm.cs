using ArticleService.Domain.Entity;

namespace ArticleService.WebApi.Controllers.Articles.ViewModel
{
    public record ArticleVm(Guid Id,string title, string Content,Guid? CategoryId,Guid AuthorId, long ViewNumber,Uri? CoverUrl,bool HasPassword,DateTime Created, DateTime Updated, string PublicField)
    {
        public static ArticleVm? Create(Article? article)
        {
            if (article == null || article.IsVisible==false)
                return null;
            if(article.HashPassword!=null)
                return new ArticleVm(article.Id, article.Title, "此内容不可查看", article.CategoryId, article.AuthorId, article.ViewNumber, article.CoverUrl,false,article.Created,article.Updated,article.OptionField.PublicField);
            return new ArticleVm(article.Id, article.Title, article.Content, article.CategoryId, article.AuthorId, article.ViewNumber, article.CoverUrl,true, article.Created, article.Updated, article.OptionField.PublicField);
        }

        public static ArticleVm[] Create(Article[] items)
        {
            return items.Where(e=>e.IsVisible==true).Select(e=>Create(e)).ToArray();
        }

        public static ArticleVm CreateSecret(Article? article)
        {
            if (article == null)
                return null;
            return new ArticleVm(article.Id, article.Title, article.Content, article.CategoryId, article.AuthorId, article.ViewNumber, article.CoverUrl, true, article.Created, article.Updated, article.OptionField.PublicField);
        }
    }
}
