using Microsoft.EntityFrameworkCore;
using MoviesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Data.Repositories
{
    public class MovieRepository: IMovieRepository
    {
        private readonly MovieContext _context;
        private readonly DbSet<Movie> _movies;

        public MovieRepository(MovieContext dbContext)
        {
            _context = dbContext;
            _movies = dbContext.Movies;
        }

        public IEnumerable<Movie> GetAll()
        {
            return _movies.Include(r => r.Actors).ToList();
        }

        public Movie GetBy(int id)
        {
            return _movies.Include(r => r.Actors).SingleOrDefault(r => r.Id == id);
        }

        public bool TryGetMovie(int id, out Movie movie)
        {
            movie = _context.Movies.Include(t => t.Actors).FirstOrDefault(t => t.Id == id);
            return movie != null;
        }

        public void Add(Movie movie)
        {
            _movies.Add(movie);
        }

        public void Update(Movie movie)
        {
            _context.Update(movie);
        }

        public void Delete(Movie movie)
        {
            _movies.Remove(movie);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
