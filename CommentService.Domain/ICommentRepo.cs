using CommentService.Domain.Entity;

namespace CommentService.Domain
{
    public interface ICommentRepo
    {
        public Task<Guid> CreateAsync(Comment comment);

        public Task<Comment?> FindByIdAsync(Guid id);

        public Task<Comment[]> GetAllAsync();

        public Task<Comment[]> FindByAuthorAsync(Guid authorId);

        public Task<Comment[]> FindBySubmitterNameAsync(string name);

        public Task<Comment[]> GetPageCommentAsync(Guid pageId);

        public Task<Comment[]> GetArticleCommentAsync(Guid articleId);


        public Task<Comment[]> GetNormalComments();

        public Task<Comment[]> GetSpamComments();

        public Task<Comment[]> GetPrivateComments();
    }
}
