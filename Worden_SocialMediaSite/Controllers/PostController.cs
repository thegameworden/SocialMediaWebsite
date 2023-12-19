using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<Data.Account> _userManager;
        public PostController(SocialMediaDbContext _dbContext, UserManager<Data.Account> _userManager)
        {
            this._dbContext = _dbContext;
            this._userManager = _userManager;

        }
   
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

            if(post.Image != null)
            {
                string imageBase64Data = Convert.ToBase64String(post.Image);
                string imageURL = string.Format("data:image/G;base64,{0}", imageBase64Data);
                ViewBag.ImageUrl = imageURL;
            }
            
            post.Comments?.Sort(new SortCommentsByLikesAscending());
            return View(post);
        }


        [Route("followingPage")]
        [Route("following")]
        [Route("ListAll")]
        [Authorize]
        public async Task<IActionResult> Following()
        {
            var loggedUser = await _userManager.GetUserAsync(User);
            var followedAccountsIds = _dbContext.Accounts
                                    .Include(a => a.Following)
                                    .FirstOrDefault(a => a.Id == loggedUser.Id)
                                    ?.Following.Select(f => f.Id).ToList();


            

             List<Post> posts = _dbContext.Posts
                          .Where(p => followedAccountsIds.Contains(p.AuthorId))
                          .Include(p => p.Author)
                          .Include(p => p.Comments)
                          .ThenInclude(c => c.Author)
                          .ToList();

            foreach (var post in posts)
            {
                post.Comments?.Sort(new SortCommentsByLikesAscending());
            }
            posts.Sort(new SortPostsByDateLatest());
            return View(posts);

        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(Post post)
        {
			ModelState.Remove("author");
            ModelState.Remove("AuthorId");
			ModelState.Remove("comments");
			if (!ModelState.IsValid)
            {
                return View("Create");
            }
            post.Author = await  _userManager.GetUserAsync(User);
            foreach (var file in Request.Form.Files)
            {
                MemoryStream memoryStream = new MemoryStream();
                file.CopyTo(memoryStream);
                post.Image = memoryStream.ToArray();

                memoryStream.Close();
                memoryStream.Dispose();
            }

            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();
           
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [Authorize]
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
            ModelState.Remove("author");
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
        [Authorize]
        public IActionResult Delete(int id)
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
