using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Worden_SocialMediaSite.Data;
using Worden_SocialMediaSite.Models;

namespace Worden_SocialMediaSite.Controllers
{
    public class CommentController : Controller
    {
        private SocialMediaDbContext _dbContext;
        public CommentController(SocialMediaDbContext _dbContext)
        {
            this._dbContext = _dbContext;

        }
        Account localAccount = new()
        {
            Id = 1

        };
        // GET: CommentController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CommentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CommentController/Create
        [HttpPost]
        public ActionResult Create(Comment comment)
        {
            ModelState.Remove("Author");
            ModelState.Remove("AuthorId");
            ModelState.Remove("Post");

            if (!ModelState.IsValid)
            {
                return RedirectToAction("details", "Post", new { id = comment.PostId });
            }
            comment.Author = _dbContext.Accounts.Find(localAccount.Id);
            comment.AuthorId = localAccount.Id;
            comment.Post = _dbContext.Posts.Find(comment.PostId);
            _dbContext.Comments.Add(comment);
            _dbContext.SaveChanges();
            return RedirectToAction("details", "Post", new { id = comment.PostId });

        }
        /*
        // POST: CommentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        */

        // GET: CommentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CommentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CommentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CommentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
