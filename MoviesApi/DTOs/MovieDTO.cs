using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.DTOs
{
    public class MovieDTO
    {
        [Required]
        public string Name { get; set; }
        public int Score { get; set; }
        public string Description { get; set; }

        public DateTime DateRelease { get; set; }
        public List<string> Categories { get; set; }
      

        public IList<ActorDTO> Actors { get; set; }
        public IList<CommentDTO> Comments { get; set; }

    }
}
