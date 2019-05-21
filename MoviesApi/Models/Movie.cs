using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Models
{
    public class Movie
    {
        #region Properties
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public int Score { get; set; }
        public string Description { get; set; }

        public DateTime DateRelease { get; set; }
        public string Categories { get; set; }

        public ICollection<Actor> Actors { get; private set; }
        public ICollection<Comment> Comments { get; private set; }

        #endregion

        #region Constructors
        public Movie()
        {
            Actors = new List<Actor>();
            Comments = new List<Comment>();
            //Score = 0;
            //Description = "";
        }

        public Movie(string name, DateTime release, int score, string description, string categories) : this()
        {
            Name = name;
            DateRelease = release;
            Score = score;
            Description = description;
            Categories = categories;
        }
        #endregion

        #region Methods
        public void AddActor(Actor actor) => Actors.Add(actor);

        public Actor GetActor(int id) => Actors.SingleOrDefault(act => act.Id == id);
        public void AddComment(Comment comment) => Comments.Add(comment);

        public Comment GetComment(int id) => Comments.SingleOrDefault(com => com.Id == id);
        #endregion
    }
}

