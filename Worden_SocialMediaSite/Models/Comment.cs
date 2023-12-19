using Worden_SocialMediaSite.CustomValidations;

namespace Worden_SocialMediaSite.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [InappropriateLanguage(ErrorMessage = "This comment contains inappropriate language.")]
        public string Text { get; set; }
        public int Likes { get; set; } = 0;

        public Data.Account Author { get; set; }
        public string AuthorId { get; set; }
        public List<Comment> Replies { get; set; } = new List<Comment>();
        public DateTime TimeCommented { get; set; } = DateTime.MinValue;
        public Post Post { get; set; }
        public int PostId { get; set; }

    }
}
