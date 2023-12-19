using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Worden_SocialMediaSite.Data;
using Worden_SocialMediaSite.Models;
using Worden_SocialMediaSite.ViewModels;

namespace Worden_SocialMediaSite.Controllers
{
    public class AccountsController : Controller
    {
        private readonly SocialMediaDbContext _context;
        //inject signInManager
        private readonly SignInManager<Data.Account> _signInManager;
        private readonly UserManager<Data.Account> _userManager;
        public AccountsController(SocialMediaDbContext context, SignInManager<Data.Account> signInManager,
                                 UserManager<Data.Account> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }


        //register
        public IActionResult Register() //this will give us the Registration form ...
        {
            return View();
        }

        [HttpPost] //use the values from the Registration form to register new user
        public async Task<IActionResult> Register(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                //attempt to register a new user account 
                Data.Account newUser = new()
                {
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    Email = registerModel.Email,
                    PhoneNumber = registerModel.PhoneNumber,
                    UserName = registerModel.UserName
                };
                var result = await _userManager.CreateAsync(newUser, registerModel.Password);

                //check the result
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            //something went wrong, go back to the register form
            return View(registerModel);
        }


        //login
        public IActionResult Login()
        {
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginmodel)
        {
           
            if (ModelState.IsValid)
            {
                //attempt to log in
                var result = await _signInManager.PasswordSignInAsync(loginmodel.UserName,
                    loginmodel.Password, loginmodel.Remember, false);

                //check it it succeeded
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            //if you get here something went wrong
            ModelState.AddModelError("", "Failed to log in");
            return View(loginmodel);
        }

        //logout
        public async Task<IActionResult> Logout()
        {
            //do the logout
            await _signInManager.SignOutAsync();
            //go to the main page ...
            return RedirectToAction("Index", "Home");
        }




    // GET: Accounts
    public async Task<IActionResult> Index()
        {
            List<Data.Account>? dAccounts = await _context.Accounts
                               .Include(a => a.Followers)
                               .Include(a => a.Following)
                               .Include(a => a.PersonalPosts)
                               .ToListAsync();
            if (dAccounts == null)
            {
                return Problem("Entity set 'SocialMediaDbContext.Accounts' is null.");
            }
            List<Models.Account>? accounts = new();
            foreach(var a in dAccounts)
            {
                accounts.Add(DataToModel(a));
            }

            

            return View(accounts);
        }

        // GET: Accounts/Details/5
        [Authorize]
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }
            Data.Account? dAccount = await EntityFrameworkQueryableExtensions.Include(_context.Accounts
, x => x.PersonalPosts)
                                            .Include(x => x.Followers)
                                            .Include(x => x.Following)
                                            .Include(x => x.PersonalPosts)
                                            .FirstOrDefaultAsync(m => m.Id == id);
            

            if (dAccount == null)
            {
                return NotFound();
            }
            Models.Account? account = DataToModel(dAccount);

            return View(account);
        }

		// GET: Accounts/Create
		[Authorize]
		public IActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,PasswordHash,Email,FirstName,LastName,PhoneNumber")] Models.Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

		// GET: Accounts/Edit/5
		[Authorize]
		public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            Data.Account? dAccount = await EntityFrameworkQueryableExtensions.Include(_context.Accounts
, x => x.PersonalPosts)
                                .Include(x => x.Followers)
                                .Include(x => x.Following)
                                .FirstOrDefaultAsync(m => m.Id == id);
            if (dAccount == null) {
                return NotFound();
            }

            Models.Account? account = DataToModel(dAccount);

            var suggestedAccounts = _context.Accounts
                                .ToList()
                                .Where(a => !account.Following.Any(f => f.Id == a.Id) && a.Id != account.Id)
                                .ToList();

            ViewBag.SuggestedAccounts= suggestedAccounts; // Passing unfollowed accounts
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,PasswordHash,Email,FirstName,LastName,PhoneNumber")] Models.Account accountModel)
        {
            if (id != accountModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var accountInDb = await _context.Accounts.FindAsync(id);
                    if (accountInDb == null)
                    {
                        return NotFound();
                    }

                    // Update the properties
                    accountInDb.UserName = accountModel.UserName;
                    accountInDb.Email= accountModel.Email;
                    accountInDb.FirstName = accountModel.FirstName;
                    accountInDb.LastName = accountModel.LastName;
                    accountInDb.PhoneNumber = accountModel.PhoneNumber;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(accountModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        // Handle the concurrency issue here
                        // For example, reload the data, log the conflict, inform the user, etc.
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(accountModel);
        }

		// GET: Accounts/Delete/5
		[Authorize]
		public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

		// POST: Accounts/Delete/5
		[Authorize]
		[HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'SocialMediaDbContext.Accounts'  is null.");
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(string id)
        {
          return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }



        [HttpPost]
        public async Task<IActionResult> FollowAccount(string userId, string followingId)
        {
            var user = await _context.Accounts
                                     .Include(a => a.Following)
                                     .FirstOrDefaultAsync(a => a.Id == userId);

            var accountToFollow = await _context.Accounts
                                                .Include(a => a.Followers)
                                                .FirstOrDefaultAsync(a => a.Id == followingId);

            if (user == null || accountToFollow == null)
            {
                return NotFound(); // Or handle the error as appropriate
            }

            // Add to the following list
            user.Following.Add(accountToFollow);

            accountToFollow.Followers.Add(user);

            await _context.SaveChangesAsync();

            return RedirectToAction("Edit", new { id = userId }); // Redirect back to the edit page
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFollower(string userId, string followerId)
        {
            var user = await _context.Accounts
                                     .Include(a => a.Followers)
                                     .FirstOrDefaultAsync(a => a.Id == userId);

            var followerToRemove = await _context.Accounts
                                                .Include(a => a.Following)
                                                .FirstOrDefaultAsync(a => a.Id == followerId);

            if (user == null || followerToRemove == null)
            {
                return NotFound(); // Or handle the error as appropriate
            }

            user.Followers.Remove(followerToRemove);

            followerToRemove.Following.Remove(user);

            await _context.SaveChangesAsync();

            return RedirectToAction("Edit", new { id = userId }); // Redirect back to the edit page
        }
        private Models.Account? DataToModel(Data.Account data)
        {
            if(data==null) return null;
            return new Models.Account
            {
                Id = data.Id,
                Comments = data.Comments,
                Email = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Followers = data.Followers,
                Following = data.Following,
                LikedPosts = data.LikedPosts,
                PasswordHash = data.PasswordHash,
                PhoneNumber = data.PhoneNumber,
                UserName = data.UserName,
                PersonalPosts = data.PersonalPosts,
            };
        }
    }


    
}
