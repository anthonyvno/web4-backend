using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Models
{
    public class Comment
    {
        #region Properties
        public int Id { get; set; }

        public string Content { get; set; }

        public string PostedBy { get; set; }
        #endregion

        #region Constructors
        public Comment(string content, string postedBy)
        {
            Content = content;
            PostedBy = postedBy;
        }
        #endregion
    }
}
