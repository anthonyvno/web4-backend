using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Models
{
    public interface IMovieRepository
    {
        Movie GetBy(int id);
        bool TryGetMovie(int id, out Movie movie);
        IEnumerable<Movie> GetAll();
        void Add(Movie movie);
        void Delete(Movie movie);
        void Update(Movie movie);
        void SaveChanges();
    }
}
