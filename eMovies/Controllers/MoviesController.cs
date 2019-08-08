using System.Collections.Generic;
using System.Web.Mvc;
using eMovies.ViewModels;
using eMovies.Models;
using eMovies.Dependencies;
using eMovies.Interfaces;

namespace eMovies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
            
        }
        public ActionResult Index()
        {
            MovieIndexViewModel vm = _movieRepository.GetAllMovies();
            return View(vm);
        }
        
        public ActionResult Edit(string id)
        {
            return Content("id=" + id);
        }

        [Route("movie/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByRealeaseDate(int year, byte month)
        {
            return Content(year + "/" + month);
        }

        public ActionResult UpdateInts(ICollection<int> ints)
        {
            return View(ints);
        }

        public ActionResult Details(int id)
        {
            MovieDetailsViewModel vm = _movieRepository.GetMovieDetails(id);
            return View(vm);
        }

        public JsonResult GetMoviesForModal(int id)
        {
            Movy movie = _movieRepository.GetModalMovies(id);
            return Json(movie);
        }

        //public JsonResult DateConv(int id)
        //{
        //    return Json(_db.Movies.Where(M => M.movie_id == id).ToList());
        //}
    }
}