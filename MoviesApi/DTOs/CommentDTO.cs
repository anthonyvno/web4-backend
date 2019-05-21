using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.DTOs
{
    public class CommentDTO
    {
        public string Content { get; set; }

        public string PostedBy { get; set; }
    }
}
