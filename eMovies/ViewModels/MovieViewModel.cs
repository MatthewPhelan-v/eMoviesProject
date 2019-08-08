using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace eMovies.ViewModels
{
    public class MovieViewModel
    {
        public int order_id { get; set; }
        public int movie_id { get; set; }
        public string movie_title { get; set; }
        public decimal price { get; set; }

        public string formattedAmount {
            get
            {
                return price.ToString("C", CultureInfo.CurrentCulture);
            }
        }
        public int quantity { get; set; }
        public string genre { get; set; }
        public int length_minutes { get; set; }
        public string image_url { get; set; }
    }
}