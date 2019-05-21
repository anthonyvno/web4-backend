using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Models
{
    public class CustomerFavorite
    {
        #region Properties
        public int CustomerId { get; set; }

        public int MovieId { get; set; }

        public Customer Customer { get; set; }

        public Movie Movie { get; set; }
        #endregion
    }
}
