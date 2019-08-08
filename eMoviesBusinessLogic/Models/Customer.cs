using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eMoviesBusinessLogic.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        private PaymentCard PaymentCard { get; set; }
    }
}