using Worden_SocialMediaSite.Models;

namespace Worden_SocialMediaSite.Controllers
{
    class SortCommentsByLikesAscending : IComparer<Comment>
    {
        public int Compare(Comment x, Comment y)
        {
            return y.Likes.CompareTo(x.Likes);
        }
    }

    class SortPostsByDateLatest : IComparer<Post>
    {
        public int Compare(Post x, Post y)
        {
            return y.TimePosted.CompareTo(x.TimePosted);
        }
    }
    class SortPostsByLikes: IComparer<Post>{
        public int Compare(Post x, Post y)
        {
            return y.Likes.CompareTo(x.Likes);
        }
    }

}
