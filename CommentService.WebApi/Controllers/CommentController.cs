using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CommentService.Domain;
using CommentService.Domain.Entity;
using CommentService.Domain.ValueObject;
using CommentService.Infrastructure;
using AspNetCore;
using CommentService.WebApi.Controllers.ViewModel;
using CommentService.WebApi.Controllers.Request;
using Microsoft.AspNetCore.Authorization;

namespace CommentService.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [UnitOfWork(typeof(CommentDbContext))]
    public class CommentController : ControllerBase
    {
        private readonly CommentDbContext _dbCtx;
        private readonly CommentDomainSer _ser;
        private readonly ICommentRepo _repo;
        private readonly IRecaptchaValidator _recaptchaValidator;
        private readonly IDistributedCacheHelper _distributedCacheHelper;
        IRecaptchaValidator _recap;

        public CommentController(CommentDbContext dbCtx, CommentDomainSer ser, ICommentRepo repo, IRecaptchaValidator recaptcha, IDistributedCacheHelper cache,IRecaptchaValidator recap)
        {
            _dbCtx = dbCtx;
            _ser = ser;
            _repo = repo;
            _recaptchaValidator = recaptcha;
            _distributedCacheHelper = cache;
            _recap = recap;
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CommentVm?>> FindNormalById(Guid id)
        {
            var comment = await _distributedCacheHelper.GetOrCreateAsync($"CommentController.FindNormalById.{id}", async (e) => await _repo.FindByIdAsync(id));
            if (comment == null || comment.Status != (int)Comment.CommentStatus.Normal)
                return NotFound();

            return CommentVm.Create(comment);
        }

        [HttpGet]
        [Route("{contentId}")]
        public async Task<ActionResult<CommentVm[]>> GetContentComment(Guid contentId, int type)
        {
            var comments = await _distributedCacheHelper.GetOrCreateAsync($"CommentController.GetContentComment", async (e) =>
            {
                return await _dbCtx.Comments.Where(x => (x.Status == (int)Comment.CommentStatus.Normal || x.Status == (int)Comment.CommentStatus.Private) && x.Type == type).ToArrayAsync();
            });

            return CommentVm.Create(comments);
        }


        [HttpPost]
        public async Task<ActionResult<Guid>> WriteComment(WriteCommentReq req)
        {
            try
            {
                if (!await _recap.ValidateAsync(req.GoogleToken))
                    return BadRequest("验证错误");

                Comment.Builder cmtBuilder = new Comment.Builder();

                cmtBuilder.Parent(req.Parent).Text(req.Text).Type(req.Type).Submitter(new Submitter(
                    req.Username,
                    req.SiteUrl,
                    req.Email,
                    HttpContext.Connection.RemoteIpAddress.ToString(),
                    HttpContext.Request.Headers["User-Agent"].ToString())).AuthorId(null);
                if (req.IsPrivate)
                    cmtBuilder.SetPrivate();
                else
                    cmtBuilder.SetNormal();
                Comment comment = cmtBuilder.Build();
                this._dbCtx.Comments.Add(comment);

                return comment.Id;

            }
            catch(Exception ex)
            {
                return BadRequest("校验超时");
            }

            
        }

        


    }
}
