using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eMoviesBusinessLogic.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int Cost { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }

    }
}