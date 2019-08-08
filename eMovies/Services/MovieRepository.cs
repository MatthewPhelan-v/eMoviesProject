using eMovies.Interfaces;
using eMovies.Models;
using eMovies.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace eMovies.Dependencies
{
    
    public class MovieRepository : IMovieRepository
    {
        private readonly emoviesEntities _db;
        public MovieRepository()
        {
            try
            {
                _db = new emoviesEntities();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public MovieIndexViewModel GetAllMovies()
        {
            List<Movy> m = _db.Movies.ToList();
            MovieIndexViewModel vm = SetupMIVM(m);

            return vm;
        }

        public MovieIndexViewModel SetupMIVM(List<Movy> m)
        {
            List<MovieViewModel> movies = new List<MovieViewModel>();

            foreach (Movy movy in m)
            {
                MovieViewModel mvm = SetupMVM(movy);
                movies.Add(mvm);
            }

            MovieIndexViewModel vm = new MovieIndexViewModel
            {
                Movies = movies
            };

            return vm;
        }

        public MovieViewModel SetupMVM(Movy movy)
        {
            MovieViewModel mvm = new MovieViewModel
            {
                movie_id = movy.movie_id,
                movie_title = movy.movie_title,
                price = movy.price,
                genre = movy.genre,
                length_minutes = movy.length_minutes,
                image_url = movy.image_url
            };

            return mvm;
        }

        public Movy GetModalMovies(int id)
        {
            Movy movie = _db.Movies.First(M => M.movie_id == id);
            return movie;
        }

        public MovieDetailsViewModel GetMovieDetails(int id)
        {
            Movy m = _db.Movies.Find(id);
            MovieDetailsViewModel vm = SetupMDVM(m);

            return vm;
        }

        public MovieDetailsViewModel SetupMDVM(Movy m)
        {
            MovieDetailsViewModel vm = new MovieDetailsViewModel
            {
                Title = m.movie_title,
                Length = m.length_minutes,
                Genre = m.genre,
                Price = m.price,
                ImgUrl = m.image_url
            };

            return vm;
        }
    }
}