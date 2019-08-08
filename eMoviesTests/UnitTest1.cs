using System;
using System.Collections.Generic;
using System.Web.Mvc;
using eMovies.Controllers;
using eMovies.Interfaces;
using eMovies.Models;
using eMovies.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace eMoviesTests
{
    [TestClass]
    public class MovieControllerTests
    {

        private Mock<IMovieRepository> _movieRepository;
        private MoviesController _controller;

        [TestInitialize]
        public void Setup()
        {
            _movieRepository = new Mock<IMovieRepository>();
            _controller = new MoviesController(_movieRepository.Object);
        }



        [TestMethod]
        public void GetModalMoviesTest()
        {
            var dt = DateTime.Now;
            double p = 12.5;
            decimal v = (decimal) p;
            int id = 4;

            Movy modalMovie = new Movy() { movie_id = id,
                                            movie_title = "National Treasure",
                                            length_minutes = 144,
                                            release_date = dt,
                                            genre = "Thriller",
                                            price = v
                                        };
            
            _movieRepository.Setup(x => x.GetModalMovies(4)).Returns(modalMovie);
            var controller = new MoviesController(_movieRepository.Object);
            var actual = controller.AJAXTest(id).Data;

            Assert.Equals(modalMovie, actual);

        }
    }
}
