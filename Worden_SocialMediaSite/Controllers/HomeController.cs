using Microsoft.AspNetCore.Mvc;
using Worden_SocialMediaSite.Data;
using Worden_SocialMediaSite.Models;
using Microsoft.EntityFrameworkCore;

namespace Worden_SocialMediaSite.Controllers
{
    public class HomeController : Controller
    {
        //private ServiceInterface postService;
        private SocialMediaDbContext _dbContext;
        public HomeController(SocialMediaDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        [Route("")]
        [Route("Home")]
        [Route("Main")]
        public IActionResult Index()
        {
            List<Post> posts = _dbContext.Posts.Include(p => p.Author).ToList();
            foreach (var post in posts)
            {
                if(post.Comments != null)
                post.Comments.Sort(new SortCommentsByLikesAscending());
            }
            posts.Sort(new SortPostsByLikes());
            return View(posts);
        }
        public IActionResult ForYouPage()
        {
            return Content("This will eventually show a for you page...");
        }

    }
}
