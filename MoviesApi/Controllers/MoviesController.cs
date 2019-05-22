using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.DTOs;
using MoviesApi.Models;

namespace MoviesApi.Controllers
{


    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ICustomerRepository _customerRepository;

        public MoviesController(IMovieRepository movieRepository, ICustomerRepository customerRepository)
        {
            _movieRepository = movieRepository;
            _customerRepository = customerRepository;
        }

        // GET: api/Movies
        /// <summary>
        /// Get all movies ordered by name
        /// </summary>
        /// <returns>array of movies</returns>
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Movie> GetMovies()
        {
            return _movieRepository.GetAll().OrderBy(r => r.Name);
        }

        // GET: api/Movies/5
        /// <summary>
        /// Get the movie with given id
        /// </summary>
        /// <param name="id">the id of the movie</param>
        /// <returns>The movie</returns>
        [HttpGet("{id}")]
        public ActionResult<Movie> GetMovie(int id)
        {
            Movie movie = _movieRepository.GetBy(id);
            if (movie == null) return NotFound();
            return movie;
        }

        /// <summary>
        /// Get favorite movies of current user
        /// </summary>
        [HttpGet("Favorites")]
        public IEnumerable<Movie> GetFavorites()
        {
            Customer customer = _customerRepository.GetBy(User.Identity.Name);
            return customer.FavoriteMovies;
        }

        // POST: api/Movies
        /// <summary>
        /// Adds a new movie
        /// </summary>
        /// <param name="movie">the new movie</param>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult<Movie> PostMovie(MovieDTO movie)
        {
            Movie movieToCreate = new Movie() { Name = movie.Name, Score = movie.Score, Description = movie.Description, DateRelease = movie.DateRelease, Picture = movie.Picture, Categories=movie.Categories };
            foreach (var a in movie.Actors)
                movieToCreate.AddActor(new Actor(a.Name, a.BirthDate));
            foreach (var c in movie.Comments)
                movieToCreate.AddComment(new Comment(c.Content,c.PostedBy));
            _movieRepository.Add(movieToCreate);
            _movieRepository.SaveChanges();

            return CreatedAtAction(nameof(GetMovie), new { id = movieToCreate.Id }, movieToCreate);
        }

        // PUT: api/Movies/5
        /// <summary>
        /// Modifies a movie
        /// </summary>
        /// <param name="id">id of the movie to be modified</param>
        /// <param name="movie">the modified movie</param>
        [HttpPut("{id}")]
        public IActionResult PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }
            _movieRepository.Update(movie);
            _movieRepository.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Movies/5
        /// <summary>
        /// Deletes a movie
        /// </summary>
        /// <param name="id">the id of the movie to be deleted</param>
        [HttpDelete("{id}")]
        public ActionResult<Movie> DeleteMovie(int id)
        {
            Movie movie = _movieRepository.GetBy(id);
            if (movie == null)
            {
                return NotFound();
            }
            _movieRepository.Delete(movie);
            _movieRepository.SaveChanges();
            return movie;
        }

        /// <summary>
        /// Get an actor for a movie
        /// </summary>
        /// <param name="id">id of the movie</param>
        /// <param name="actorId">id of the actor</param>
        [HttpGet("{id}/actors/{actorId}")]
        public ActionResult<Actor> GetActor(int id, int actorId)
        {
            if (!_movieRepository.TryGetMovie(id, out var movie))
            {
                return NotFound();
            }
            Actor actor = movie.GetActor(actorId);
            if (actor == null)
                return NotFound();
            return actor;
        }

        /// <summary>
        /// Adds an actor to a movie
        /// </summary>
        /// <param name="id">the id of the movie</param>
        /// <param name="actor">the actor to be added</param>
        [HttpPost("{id}/actors")]
        public ActionResult<Actor> PostActor(int id, ActorDTO actor)
        {
            if (!_movieRepository.TryGetMovie(id, out var movie))
            {
                return NotFound();
            }
            var actorToCreate = new Actor(actor.Name, actor.BirthDate);
            movie.AddActor(actorToCreate);
            _movieRepository.SaveChanges();
            return CreatedAtAction("GetActor", new { id = movie.Id, actorId = actorToCreate.Id }, actorToCreate);
        }

        /// <summary>
        /// Get an comment for a movie
        /// </summary>
        /// <param name="id">id of the movie</param>
        /// <param name="commentId">id of the comment</param>
        [HttpGet("{id}/comments/{commentId}")]
        public ActionResult<Comment> GetComment(int id, int commentId)
        {
            if (!_movieRepository.TryGetMovie(id, out var movie))
            {
                return NotFound();
            }
            Comment comment = movie.GetComment(commentId);
            if (comment == null)
                return NotFound();
            return comment;
        }

        /// <summary>
        /// Adds an comment to a movie
        /// </summary>
        /// <param name="id">the id of the movie</param>
        /// <param name="comment">the comment to be added</param>
        [HttpPost("{id}/comments")]
        public ActionResult<Comment> PostComment(int id, CommentDTO comment)
        {
            if (!_movieRepository.TryGetMovie(id, out var movie))
            {
                return NotFound();
            }
            var commentToCreate = new Comment(comment.Content, comment.PostedBy);
            movie.AddComment(commentToCreate);
            _movieRepository.SaveChanges();
            return CreatedAtAction("GetComment", new { id = movie.Id, commentId = commentToCreate.Id }, commentToCreate);
        }

    }
}