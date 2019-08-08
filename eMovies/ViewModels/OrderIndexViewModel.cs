using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eMovies.ViewModels
{
    public class OrderIndexViewModel
    {
        public ICollection<MovieViewModel> selection { get; set; }
        public int orderId { get; set; }
    }
}