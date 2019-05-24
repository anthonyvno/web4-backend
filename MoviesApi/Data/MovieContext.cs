using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Data
{
    public class MovieContext : IdentityDbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Movie>()
                .HasMany(p => p.Actors)
                .WithOne()
                .IsRequired()
                .HasForeignKey("MovieId"); //Shadow property
            builder.Entity<Movie>()
                .HasMany(p => p.Comments)
                .WithOne()
                .IsRequired()
                .HasForeignKey("MovieId"); //Shadow property
            builder.Entity<Movie>().Property(r => r.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Movie>().Property(r => r.Description).HasMaxLength(50);
            builder.Entity<Movie>().Property(r => r.Score);
            builder.Entity<Movie>().Property(r => r.DateRelease);
            builder.Entity<Movie>().Property(r => r.Categories);
            builder.Entity<Movie>().Property(r => r.Picture);




            builder.Entity<Actor>().Property(r => r.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Actor>().Property(r => r.BirthDate);

            builder.Entity<Comment>().Property(r => r.Content).IsRequired().HasMaxLength(50);
            builder.Entity<Comment>().Property(r => r.PostedBy).IsRequired();

            builder.Entity<Customer>().Property(c => c.LastName).IsRequired().HasMaxLength(50);
            builder.Entity<Customer>().Property(c => c.FirstName).IsRequired().HasMaxLength(50);
            builder.Entity<Customer>().Property(c => c.Email).IsRequired().HasMaxLength(100);
            builder.Entity<Customer>().Ignore(c => c.FavoriteMovies);

            builder.Entity<CustomerFavorite>().HasKey(f => new { f.CustomerId, f.MovieId });
            builder.Entity<CustomerFavorite>().HasOne(f => f.Customer).WithMany(u => u.Favorites).HasForeignKey(f => f.CustomerId);
            builder.Entity<CustomerFavorite>().HasOne(f => f.Movie).WithMany().HasForeignKey(f => f.MovieId);

            //Another way to seed the database
            builder.Entity<Movie>().HasData(
                new Movie { Id = 1, Name = "Titanic", Description = "droevige film", DateRelease = DateTime.Now, Score = 10, Categories = "Romance;Comedy",
                    Picture = "https://m.media-amazon.com/images/M/MV5BMDdmZGU3NDQtY2E5My00ZTliLWIzOTUtMTY4ZGI1YjdiNjk3XkEyXkFqcGdeQXVyNTA4NzY1MzY@._V1_.jpg"
                }
                //new Movie { Id = 2, Name = "Tomato soup", Created = DateTime.Now }
  );

            builder.Entity<Actor>().HasData(
                    //Shadow property can be used for the foreign key, in combination with anaonymous objects
                    new { Id = 1, Name = "Leonardo DiCaprio", BirthDate = DateTime.Now, MovieId = 1 }
                 // new { Id = 2, Name = "Minced Meat", Amount = (double?)500, Unit = "grams", MovieId = 1 },
                 //new { Id = 3, Name = "Onion", Amount = (double?)2, MovieId = 1 }
                 );
           /* builder.Entity<Comment>().HasData(
                    //Shadow property can be used for the foreign key, in combination with anaonymous objects
                    new { Id = 1, Content = "Dat was een leuke film", PostedBy="Mark", MovieId = 1 }
                 // new { Id = 2, Name = "Minced Meat", Amount = (double?)500, Unit = "grams", MovieId = 1 },
                 //new { Id = 3, Name = "Onion", Amount = (double?)2, MovieId = 1 }
                 );*/
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
