using Microsoft.AspNetCore.Identity;
using Worden_SocialMediaSite.Models;

namespace Worden_SocialMediaSite.Data
{
    public class Account : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Account> Following { get; set; } = new List<Account>();
        public List<Account> Followers { get; set; } = new List<Account>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Post> LikedPosts { get; set; } = new List<Post>();
        public List<Post> PersonalPosts { get; set; } = new List<Post>();


    }
}
