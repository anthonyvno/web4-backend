using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Models
{
    public class Customer
    {
        #region Properties
        //add extra properties if needed
        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public ICollection<CustomerFavorite> Favorites { get; private set; }

        public IEnumerable<Movie> FavoriteMovies => Favorites.Select(f => f.Movie);
        #endregion

        #region Constructors
        public Customer()
        {
            Favorites = new List<CustomerFavorite>();
        }
        #endregion

        #region Methods
        public void AddFavoriteMovie(Movie movie)
        {
            Favorites.Add(new CustomerFavorite() { MovieId = movie.Id, CustomerId = CustomerId, Movie = movie, Customer = this });
        }
        #endregion
    }
}

