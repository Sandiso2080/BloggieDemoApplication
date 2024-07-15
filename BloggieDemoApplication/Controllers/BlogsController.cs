using BloggieDemoApplication.Models.ViewModels;
using BloggieDemoApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BloggieDemoApplication.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogPostLikeRepository blogPostLikeRepository;

        public BlogsController(IBlogPostRepository blogPostRepository, IBlogPostLikeRepository blogPostLikeRepository) 
        {
            this.blogPostRepository = blogPostRepository;
            this.blogPostLikeRepository = blogPostLikeRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
          var blogPost = await blogPostRepository.GetByUrlAsync(urlHandle);
          var blogDetailsLikeViewModel = new BlogDetailsViewModel();


            if (blogPost != null) 
            {
               var totalLikes = await blogPostLikeRepository.GetTotalLikes(blogPost.Id);
               blogDetailsLikeViewModel = new BlogDetailsViewModel
                {
                    Id = blogPost.Id,
                    Content = blogPost.Content,
                    PageTitle = blogPost.PageTitle,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    Heading = blogPost.Heading,
                    PublishedDate = blogPost.PublishedDate,
                    ShortDescription = blogPost.ShortDescription,
                    UrlHandle = urlHandle,
                    Visible = blogPost.Visible,
                    Tags = blogPost.Tags,
                    TotalLikes = totalLikes
                };
            }
            return View(blogDetailsLikeViewModel);
        }
    }
}
