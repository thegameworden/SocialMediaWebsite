using Worden_SocialMediaSite.Models;
using Microsoft.EntityFrameworkCore;

namespace Worden_SocialMediaSite.Data
{
    public class SocialMediaDbContext : DbContext
    {
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Comment> Comments => Set<Comment>();
        public SocialMediaDbContext(DbContextOptions<SocialMediaDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        
            {base.OnModelCreating(modelBuilder);
            // Configure relationship between Account and Post
            modelBuilder.Entity<Account>()
                .HasMany(a => a.PersonalPosts)  // One-to-Many from Account to Post
                .WithOne(p => p.Author)       // Each Post is related to one Account
                .HasForeignKey(p => p.AuthorId);  // The foreign key in Post is AccountId
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Comments)
                .WithOne(c => c.Author)
                .HasForeignKey(c => c.AuthorId);

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c=> c.PostId);


                
            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    Id = 1,
                    Username = "ethanworden",
                    FirstName = "Ethan",
                    LastName = "Worden",
                    Email="email@gmail.com",
                    Password = "1234"

                },
                new Account
                {
                    Id= 2,
                    Username = "tommyJoe-123",
                    FirstName = "Thomas",
                    LastName = "Joe",
                    Email = "email@gmail.com",
                    Password = "1234"
                },
                new Account
                {   
                    Id=3,
                    Username = "user1",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "email@gmail.com",
                    Password = "1234"
                },
                new Account
                {
                    Id=4,
                    Username = "user2",
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "email@gmail.com",
                    Password = "1234"
                },
                new Account
                {
                    Id=5,
                    Username = "user3",
                    FirstName = "Alice",
                    LastName = "Wonders",
                    Email = "email@gmail.com",
                    Password = "1234"
                },
                new Account
                {
                    Id=6,
                    Username = "user4",
                    FirstName = "Bob",
                    LastName = "Builder",
                    Email = "email@gmail.com",
                    Password = "1234"
                },
                new Account
                {
                    Id=7,
                    Username = "user5",
                    FirstName = "Charlie",
                    LastName = "Chocolate",
                    Email = "email@gmail.com",
                    Password = "1234"
                }
                );

            
            modelBuilder.Entity<Post>().HasData(
                
                new Post
                {
                    Id = 1,
                    Caption = "This is the post caption for post B",
                    Likes = 30456,
                    TimePosted = new DateTime(2023, 10, 6, 3, 56, 0),
                    AuthorId = 1
                  
                },
                new Post
                {
                    Id = 2,
                    Caption = "Caption for post 1",
                    Likes = 50,
                    AuthorId = 2,
                    TimePosted = new DateTime(2022, 5, 10, 12, 30, 0)
                },
                new Post
                {
                    Id = 3,
                    Caption = "Caption for post 2",
                    Likes = 70,
                    TimePosted = new DateTime(2022, 5, 11, 14, 25, 0),
                    AuthorId = 3
                },
                new Post
                {
                    Id = 4,
                    Caption = "Caption for post 3",
                    Likes = 45,
                    TimePosted = new DateTime(2022, 5, 12, 16, 20, 0),
                    AuthorId = 4
                },
                new Post
                {
                    Id = 5,
                    Caption = "Caption for post 4",
                    Likes = 90,
                    TimePosted = new DateTime(2022, 5, 13, 18, 15, 0),
                    AuthorId= 5
                },
                new Post
                {
                    Id = 6,
                    Caption = "Caption for post 5",
                    Likes = 30,
                    TimePosted = new DateTime(2022, 5, 14, 20, 10, 0),
                    AuthorId = 6
                },
                new Post
                 {
                     Id = 7,
                     Caption = "This is the post caption for post A",
                     Likes = 27,
                     TimePosted = new DateTime(2023, 1, 1, 0, 0, 0),
                     AuthorId = 7
                 }
                );

            modelBuilder.Entity<Comment>().HasData(
                new Comment
                {
                    Id=4,
                    Text = "WOW GREAT CONTENT",
                    Likes = 10,
                    PostId = 1,
                    AuthorId=2
                },
                new Comment
                {
                    Id =5,
                    Text = "BOoooooooooOOOOOOoooooo",
                    Likes = 4,
                    PostId=1,
                    AuthorId = 1
                },
                new Comment
                {
                    Id=6,
                    Text = "try /1 if you haven't already...",
                    Likes = 987645,
                    PostId=1,
                    AuthorId = 1
                },
 
                new Comment { 
                    PostId=2, 
                    Id=7, 
                    Text = "Great post!", 
                    Likes = 15, 
                    AuthorId = 1
                },
                   
                new Comment {
                    PostId=3, 
                    Id=8, 
                    Text = "Loved it!",
                    Likes = 20,
                    AuthorId = 1
                },

                new Comment {
                    PostId=4, 
                    Id=9, 
                    Text = "Amazing!", 
                    Likes = 10 ,
                    AuthorId = 1 
                },

                new Comment {
                    PostId=5, 
                    Id =10,
                    Text = "Fantastic post!",
                    Likes = 25 , 
                    AuthorId = 1 
                },


                new Comment {
                    PostId=6, 
                    Id= 11,
                    Text = "Keep it up!", 
                    Likes = 5 , 
                    AuthorId = 1 
                },

                new Comment
                {
                    PostId=7,
                    Id=1,
                    Text = "This is one comment",
                    Likes = 10,
                    AuthorId = 1
                },
                new Comment
                {
                    PostId=7,
                    Id=2,
                    Text = "This is another comment",
                    Likes = 4,
                    AuthorId = 1
                },
                new Comment
                {
                    PostId=7,
                    Id=3,
                    Text = "try /1 if you haven't already...",
                    Likes = 987645,
                    AuthorId = 1
                }
               );

        }
    }

}
