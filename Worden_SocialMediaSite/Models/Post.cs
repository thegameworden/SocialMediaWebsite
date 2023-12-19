using System.ComponentModel.DataAnnotations;
using Worden_SocialMediaSite.Controllers;
using Worden_SocialMediaSite.CustomValidations;

namespace Worden_SocialMediaSite.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Display(Name ="Account: ")]
        public Data.Account Author { get; set; }
        public string AuthorId { get; set; }
        [Display(Name = "Caption: ")]

        [Required(ErrorMessage ="Caption is required!")]
        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        [InappropriateLanguage(ErrorMessage="This post contains inappropriate language.")]
        public string Caption { get; set; }
        [Display(Name = "Likes: ")]

        [Required]
		[Range(0, int.MaxValue, ErrorMessage = "Likes cannot be less than 0.")]
		public int Likes { get; set; } = 0;

        [Display(Name = "Comments: ")]
        public List<Comment> Comments {  get; set; } = new List<Comment>();

        [Display(Name = "Time Posted: ")]
        [Required(ErrorMessage ="Date Must be between the years 2000 and 3000")]
        [Range(typeof(DateTime),"1/1/2000","1/1/3000")]
        public DateTime TimePosted { get; set; } = DateTime.Now;


        [Display(Name = "Image:")]
        public byte[]? Image { get; set; }





    }
}
