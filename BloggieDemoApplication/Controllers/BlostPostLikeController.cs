using BloggieDemoApplication.Models.Domain;
using BloggieDemoApplication.Models.ViewModels;
using BloggieDemoApplication.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloggieDemoApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlostPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepository blogPostLikeRepository;

        public BlostPostLikeController(IBlogPostLikeRepository blogPostLikeRepository) 
        {
            this.blogPostLikeRepository = blogPostLikeRepository;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
        {
            var model = new BlogPostLike
            {
                BlogPostId = addLikeRequest.BlogPostId,
                UserId = addLikeRequest.UserId,

            };
               await blogPostLikeRepository.AddLikeForBlog(model);
            return Ok();
        }

        [HttpGet]
        [Route("{blogPostId:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikesForBlog([FromRoute] Guid blogPostId)
        {
            var totalLikes = await blogPostLikeRepository.GetTotalLikes(blogPostId);

            return Ok(totalLikes);
        }
    }
}
