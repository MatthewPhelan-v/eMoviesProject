using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eMovies.ViewModels
{
    public class PaymentCompletionViewModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public string card_number {get; set;}
        public List<OrderViewModel> movies_orders { get; set; }
        public decimal total_cost { get; set; }
    }
}