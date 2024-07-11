﻿using BloggieDemoApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BloggieDemoApplication.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;

        public BlogsController(IBlogPostRepository blogPostRepository) 
        {
            this.blogPostRepository = blogPostRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
          var blogPost = await blogPostRepository.GetByUrlAsync(urlHandle);

            return View(blogPost);
        }
    }
}
