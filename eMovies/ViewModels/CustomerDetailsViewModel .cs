using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eMoviesBusinessLogic.Models;

namespace eMovies.ViewModels
{
    public class CustomerDetailsViewModel
    {
        public int customer_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public byte age { get; set; }
        public string email { get; set; }
        public string number { get; set; }
    }
}