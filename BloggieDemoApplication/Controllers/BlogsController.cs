using BloggieDemoApplication.Models.Domain;
using BloggieDemoApplication.Models.ViewModels;
using BloggieDemoApplication.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BloggieDemoApplication.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogPostLikeRepository blogPostLikeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IBlogPostCommentRepository blogPostCommentRepository;

        public BlogsController(IBlogPostRepository blogPostRepository, IBlogPostLikeRepository blogPostLikeRepository,
            SignInManager<IdentityUser>signInManager, UserManager<IdentityUser> userManager, IBlogPostCommentRepository blogPostCommentRepository) 
        {
            this.blogPostRepository = blogPostRepository;
            this.blogPostLikeRepository = blogPostLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.blogPostCommentRepository = blogPostCommentRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
          var liked = false;
          var blogPost = await blogPostRepository.GetByUrlAsync(urlHandle);
          var blogDetailsLikeViewModel = new BlogDetailsViewModel();


            if (blogPost != null) 
            {
               var totalLikes = await blogPostLikeRepository.GetTotalLikes(blogPost.Id);
                if(signInManager.IsSignedIn(User))
                {
                    // get like from this nlog
                    var likesForBlog = await blogPostLikeRepository.GetLikesForBlog(blogPost.Id);
                    var userId = userManager.GetUserId(User);
                    if (userId != null)
                    {
                      var likeFromUser =  likesForBlog.FirstOrDefault(x => x.UserId==Guid.Parse(userId));
                      liked = likeFromUser != null;
                    }
                }
                var blogCommentsDomainModel = await blogPostCommentRepository.GetCommentsByBlogIdAsync(blogPost.Id);
                var BlogCommentsForView = new List<BlogComment>();
                foreach (var blogComment in blogCommentsDomainModel) 
                {
                    BlogCommentsForView.Add(new BlogComment
                    {
                        Description = blogComment.Description,
                        DateAdded = blogComment.DataAdded,
                        Username = (await userManager.FindByIdAsync(blogComment.UserId.ToString())).UserName

                    });
                }

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
                    TotalLikes = totalLikes,
                    Liked = liked,
                    Comments = BlogCommentsForView
                };
            }
            return View(blogDetailsLikeViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Index(BlogDetailsViewModel blogDetailsViewModel)
        {
            if(signInManager.IsSignedIn(User))
            {
                var domainModel = new BlogPostComment
                {
                    BlogPostId = blogDetailsViewModel.Id,
                    Description = blogDetailsViewModel.CommentDescription,
                    UserId = Guid.Parse(userManager.GetUserId(User)),
                    DataAdded = DateTime.Now,
                };
                     await blogPostCommentRepository.AddAsync(domainModel);
                return RedirectToAction("Index", "blogs", 
                    new { urlHandle = blogDetailsViewModel.UrlHandle});
            }   
            return View();
           
            
        }
    }
}
