using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using CommentService.Domain.Entity;
using CommentService.Domain.ValueObject;
using CommentService.Domain;
using CommentService.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using AspNetCore;
using CommentService.Admin.Api.Controllers.Request;
using System.Security.Claims;

namespace CommentService.Admin.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    [UnitOfWork(typeof(CommentDbContext))]
    public class CommentController : ControllerBase
    {
        private readonly CommentDomainSer _ser;
        private readonly ICommentRepo _repo;
        private readonly CommentDbContext _dbCtx;

        public CommentController(CommentDomainSer ser, ICommentRepo repo, CommentDbContext dbCtx)
        {
            this._ser = ser;
            this._repo = repo;
            this._dbCtx = dbCtx;
        }


        [HttpPost]
        public async Task<ActionResult<Guid>> Reply(Guid id, ReplyReq req)
        {
            Comment? comment = await this._repo.FindByIdAsync(id);
            if (comment == null)
                return BadRequest("回复失败，评论不存在");
            Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (userId == null || userId == Guid.Empty)
                return BadRequest("非法用户");

            string username = this.User.FindFirstValue(ClaimTypes.Name);
            string email = this.User.FindFirstValue(ClaimTypes.Email);

            Comment.Builder cmtBuilder = new Comment.Builder();
            cmtBuilder.Type(comment.Type).SetNormal().Text(req.Text).ContentId(comment.ContentId).AuthorId(userId).Parent(comment.Id).Submitter(
                new Submitter(username, null, email, HttpContext.Connection.RemoteIpAddress.ToString(),
                    HttpContext.Request.Headers["User-Agent"].ToString()));
            Comment newComment = cmtBuilder.Build();
            await this._dbCtx.AddAsync(cmtBuilder);
            return newComment.Id;
        }

        [HttpDelete]
        [Route("id")]
        public async Task<ActionResult> Delete(Guid id)
        {
            bool flag = await this._ser.DeleteAsync(id);
            if (!flag)
                BadRequest("删除失败");
            return Ok();
        }


        [HttpGet]
        public async Task<ActionResult<Comment[]>> GetNormal()
        {
            return await this._repo.GetNormalComments();
        }

        [HttpGet]
        public async Task<ActionResult<Comment[]>> GetSpam()
        {
            return await this._repo.GetSpamComments();
        }

        [HttpGet]
        public async Task<ActionResult<Comment[]>> GetPrivate()
        {
            return await this._repo.GetPrivateComments();
        }



    }
}
