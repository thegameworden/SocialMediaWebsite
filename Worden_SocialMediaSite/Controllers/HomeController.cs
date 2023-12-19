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
        public IActionResult Index(string searchByContent, string searchByUser)
        {
            List<Post> posts = _dbContext.Posts.Include(p => p.Author).ToList();
            if (! String.IsNullOrEmpty(searchByContent))
            {
                posts = posts.Where(p => p.Caption.ToUpper().Contains(searchByContent.Trim().ToUpper())).ToList();
            }
            if(!String.IsNullOrEmpty(searchByUser))
            {

                posts = posts.Where(p=>p.Author.UserName.ToUpper().Contains(searchByUser.Trim().ToUpper())).ToList();
            }

            foreach (var post in posts)
            {
                post.Comments?.Sort(new SortCommentsByLikesAscending());
            }
            posts.Sort(new SortPostsByLikes());

            ViewBag.searchByUser = searchByUser;
            ViewBag.searchByContent = searchByContent;
            return View(posts);
        }
        public IActionResult ForYouPage()
        {
            return Content("This will eventually show a for you page...");
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}
