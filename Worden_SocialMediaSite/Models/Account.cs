namespace Worden_SocialMediaSite.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Phone { get; set; }
        public List<Account>? Following { get; set; }
        public List<Account>? Followers { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<Post>? LikedPosts { get; set; }
        public List<Post>? PersonalPosts { get; set; }
    }
}
