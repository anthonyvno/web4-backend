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
        public List<string> Categories { get; set; }

        public ICollection<Actor> Actors { get; private set; }
        #endregion

        #region Constructors
        public Movie()
        {
            Actors = new List<Actor>();
            //Score = 0;
            //Description = "";
        }

        public Movie(string name, DateTime release, int score, string description, List<string> categories) : this()
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
        #endregion
    }
}

