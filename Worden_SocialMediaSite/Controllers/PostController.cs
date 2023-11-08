using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Worden_SocialMediaSite.Data;
using Worden_SocialMediaSite.Models;

namespace Worden_SocialMediaSite.Controllers
{
    public class PostController : Controller
    {
        //private ServiceInterface postService;
        private SocialMediaDbContext _dbContext;
        public PostController(SocialMediaDbContext _dbContext)
        {
            this._dbContext = _dbContext;

        }
        Account localAccount = new()
        {
            Id = 1

        };

        public IActionResult Index()
        {
            return Content("This part of the website is still under development... Not sure what to put here tbh...");
        }


        [Route("{action}/{id?}")]
        public IActionResult Details(int id)
        {
            Post? post = _dbContext.Posts.Include(p => p.Author)
                                         .Include(p => p.Comments)
                                            .ThenInclude(c=>c.Author)
                                         .FirstOrDefault(post => post.Id == id);
            if (post == null) return NotFound();
            ViewBag.Id = id;
            if(post.Comments!=null)
            post.Comments.Sort(new SortCommentsByLikesAscending());
            return View(post);
        }


        [Route("followingPage")]
        [Route("following")]
        [Route("ListAll")]
        public IActionResult Following()
        {
            List<Post> posts = _dbContext.Posts.Include(p => p.Author).Include(p => p.Comments).ToList();
            foreach (var post in posts)
            {
                if(post.Comments!=null)
                post.Comments.Sort(new SortCommentsByLikesAscending());
            }
            posts.Sort(new SortPostsByDateLatest());
            return View(posts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPost(Post post)
        {
			ModelState.Remove("author");
			ModelState.Remove("comments");
			if (!ModelState.IsValid)
            {
                return View("Create");
            }
            post.Author = _dbContext.Accounts.Find(localAccount.Id);
            post.Comments = new List<Comment>();
            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();
            return RedirectToAction("following");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            Post? post = _dbContext.Posts.Include(p => p.Author).FirstOrDefault(post => post.Id == id);
            if (post == null) return NotFound();
            return View(post);
        }

        [HttpPost]
        public IActionResult EditPost(Post changedPost)
        {
            Post? post = _dbContext.Posts.Include(p => p.Author)
                                         .FirstOrDefault(post => post.Id == changedPost.Id);
            if (!ModelState.IsValid)
            {
                return View("Edit");
            }
            if (post == null) return NotFound(); 

            post.Caption = changedPost.Caption;
            post.Likes = changedPost.Likes;
            post.TimePosted = changedPost.TimePosted;
            _dbContext.SaveChanges();
            return RedirectToAction("following");
        }

        [HttpGet]
        public IActionResult delete(int id)
        {

            Post? post = _dbContext.Posts.FirstOrDefault(post => post.Id == id);
            if (post == null) return NotFound();
            return View(post);

        }
        public IActionResult DeleteConfirmed(int id)
        {
            Post? post = _dbContext.Posts.FirstOrDefault(post => post.Id == id);
            if (post == null) return NotFound();
            _dbContext.Posts.Remove(post);
            _dbContext.SaveChanges();
            return RedirectToAction("following");

        }
        
    }




}
