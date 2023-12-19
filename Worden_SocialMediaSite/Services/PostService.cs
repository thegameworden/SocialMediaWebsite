using Worden_SocialMediaSite.Models;

namespace Worden_SocialMediaSite.Services
{
    public class PostService : ServiceInterface
    {
        public List<Post> allPosts { get; set; }

        public PostService() {
            /*
            allPosts = new List<Post>
            {
                new Post
                {
                    Id=0,
                    Caption = "This is the post caption for post A",
                    Likes = 27,
                    TimePosted = new DateTime(2023, 1, 1, 0,0,0),
                    Author = new Account
                    {
                        UserName = "ethanworden",
                        FirstName = "Ethan",
                        LastName = "Worden"
                    },
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                            Text = "This is one comment",
                            Likes = 10,
                            AuthorId="1"
                        },
                        new Comment
                        {
                            Text = "This is another comment",
                            Likes = 4,
                            AuthorId="1"
                        },
                        new Comment
                        {
                            Text = "try /1 if you haven't already...",
                            Likes = 987645,
                            AuthorId="1"
                        }
                    }
                },
                new Post
                {
                    Id=1,
                    Caption = "This is the post caption for post B",
                    Likes = 30456,
                    TimePosted = new DateTime(2023, 10, 6, 3, 56, 0),
                    Author = new Account
                    {
                        UserName = "user1",
                        FirstName = "John",
                        LastName = "Doe"
                    },
                    Comments = new List<Comment>
                    {
                        new Comment
                        {
                            Text = "WOW GREAT CONTENT",
                            Likes = 10,
                            AuthorId=1
                        },
                        new Comment
                        {
                            Text = "BOoooooooooOOOOOOoooooo",
                            Likes = 4,
                            AuthorId = 1
                        },
                        new Comment
                        {
                            Text = "try /1 if you haven't already...",
                            Likes = 987645,
                            AuthorId = 1
                        }
                    }
                },
                new Post
                {
                    Id=2,
                    Caption = "Caption for post 1",
                    Likes = 50,
                    TimePosted = new DateTime(2022, 5, 10, 12, 30, 0),
                    Author = new Account
                    {
                        UserName = "user1",
                        FirstName = "John",
                        LastName = "Doe"
                    },
                    Comments = new List<Comment>
                    {
                        new Comment { Text = "Great post!", Likes = 15, AuthorId = 1 },
                    }
                },
                new Post
                {
                    Id=3,
                    Caption = "Caption for post 2",
                    Likes = 70,
                    TimePosted = new DateTime(2022, 5, 11, 14, 25, 0),
                    Author = new Account
                    {
                        UserName = "user2",
                        FirstName = "Jane",
                        LastName = "Smith"
                    },
                    Comments = new List<Comment>
                    {
                        new Comment { Text = "Loved it!", Likes = 20, AuthorId = 1 },
                    }
                },
                new Post
                {
                    Id=4,
                    Caption = "Caption for post 3",
                    Likes = 45,
                    TimePosted = new DateTime(2022, 5, 12, 16, 20, 0),
                    Author = new Account
                    {
                        UserName = "user3",
                        FirstName = "Alice",
                        LastName = "Wonders"
                    },
                    Comments = new List<Comment>
                    {
                        new Comment { Text = "Amazing!", Likes = 10, AuthorId = 1 },
                    }
                },
                new Post
                {
                    Id=5,
                    Caption = "Caption for post 4",
                    Likes = 90,
                    TimePosted = new DateTime(2022, 5, 13, 18, 15, 0),
                    Author = new Account
                    {
                        UserName = "user4",
                        FirstName = "Bob",
                        LastName = "Builder"
                    },
                    Comments = new List<Comment>
                    {
                        new Comment { Text = "Fantastic post!", Likes = 25, AuthorId = 1 },
                    }
                },
                new Post
                {
                    Id=6,
                    Caption = "Caption for post 5",
                    Likes = 30,
                    TimePosted = new DateTime(2022, 5, 14, 20, 10, 0),
                    Author = new Account
                    {
                        UserName = "user5",
                        FirstName = "Charlie",
                        LastName = "Chocolate"
                    },
                    Comments = new List<Comment>
                    {
                        new Comment { Text = "Keep it up!", Likes = 5, AuthorId = 1 },
                    }
                }
            };
            */
        }

    }
}
