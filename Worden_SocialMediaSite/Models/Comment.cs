using Worden_SocialMediaSite.CustomValidations;

namespace Worden_SocialMediaSite.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [InappropriateLanguage(ErrorMessage = "This comment contains inappropriate language.")]
        public string Text { get; set; }
        public int Likes { get; set; }

        public Account Author { get; set; }
        public int AuthorId { get; set; }


        public Post Post { get; set; }
        public int PostId { get; set; }

    }
}
