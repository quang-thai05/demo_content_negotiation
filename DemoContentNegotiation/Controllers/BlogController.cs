using DemoContentNegotiation.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoContentNegotiation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        [HttpGet]
        public ActionResult<Blog> Get()
        {
            var blogs = new List<Blog>();
            var blogPosts = new List<BlogPost>();

            blogPosts.Add(new BlogPost
            {
                Title = "Content Negotiation Post Title",
                MetaDescription = "Content Negotiation Meta Description",
                Published = true
            });

            blogs.Add(new Blog
            {
                Name = "PRN231 Blog",
                Description = "PRN231 Blog Desc",
                BlogPosts = blogPosts
            });

            return Ok(blogs);
        }
    }
}
