using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eMovies.ViewModels
{
    public class CardViewModel
    {
        public string card_name { get; set; }
        public string card_type { get; set; }
        public string card_number { get; set; }
        public byte expiry_month { get; set; }
        public byte expiry_year { get; set; }
        public string cvv { get; set; }
        public decimal total_price { get; set; }
        public List<int> movie_id { get; set; }
    }
}