using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eMovies.ViewModels
{
    public class OrderViewModel
    {
        public string movie_title { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
    }
}