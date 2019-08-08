using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace eMovies.ViewModels
{
    public class SignInViewModel
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}