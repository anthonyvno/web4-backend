using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Models
{
    public class Actor
    {
        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }
        #endregion

        #region Constructors
        public Actor(string name, DateTime birthDate)
        {
            Name = name;
            BirthDate = birthDate;
        }
        #endregion
    }
}
