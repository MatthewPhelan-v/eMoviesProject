using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eMoviesBusinessLogic.Models;


namespace eMovies.ViewModels
{
    public class CustomerOrderViewModel
    {
        public List<Order> Orders { get; set; }
    }
}