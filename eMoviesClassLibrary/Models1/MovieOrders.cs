using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eMoviesBusinessLogic.Models
{
    public class MovieOrders
    {
        public int MOId { get; set; }
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
        public int Quantity { get; set; }
    }
}