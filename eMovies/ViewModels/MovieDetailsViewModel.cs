using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eMovies.ViewModels
{
    public class MovieDetailsViewModel
    {
        //public int Id { get; set; }
        public string Title { get; set; }
        public int Length { get; set; }
        //public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public Decimal Price { get; set; }
        public string ImgUrl { get; set; }
        //public string Description { get; set; }
    }
}