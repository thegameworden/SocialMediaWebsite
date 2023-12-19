using Microsoft.AspNetCore.Identity;

namespace Worden_SocialMediaSite.Models
{
    public class Account
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }


        public List<Data.Account> Following { get; set; } = new List<Data.Account>();
        public List<Data.Account> Followers { get; set; } = new List<Data.Account>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Post> LikedPosts { get; set; } = new List<Post>();
        public List<Post> PersonalPosts { get; set; } = new List<Post>();
    }
}
