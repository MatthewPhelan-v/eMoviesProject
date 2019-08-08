using eMovies.Models;
using eMovies.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMovies.Interfaces
{
    public interface IMovieRepository
    {
        MovieIndexViewModel GetAllMovies();
        Movy GetModalMovies(int id);//
        MovieDetailsViewModel GetMovieDetails(int id);//
    }
}
