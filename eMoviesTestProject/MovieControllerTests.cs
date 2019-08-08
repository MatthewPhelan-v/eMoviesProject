using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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
        }

        [TestMethod]
        public void Test()
        {
            Assert.IsTrue(true);
        }
                
        [TestMethod]
        public void IndexModelReturnsCorrectType ()
        {
            MovieIndexViewModel mivm = new MovieIndexViewModel();

            _movieRepository.Setup(x => x.GetAllMovies()).Returns(mivm);
            _controller = new MoviesController(_movieRepository.Object);

            var result = _controller.Index() as ViewResult;

            Assert.AreEqual(result.Model.GetType(), mivm.GetType());
        }

        [TestMethod]
        public void OrderDetailsReturnsCorrectModelType()
        {
            MovieDetailsViewModel mivm = new MovieDetailsViewModel();

            _movieRepository.Setup(x => x.GetMovieDetails(1)).Returns(mivm);
            _controller = new MoviesController(_movieRepository.Object);

            var result = _controller.Details(1) as ViewResult;

            Assert.AreEqual(mivm.GetType(), result.Model.GetType());
        }

        [TestMethod]
        public void DetailsViewData()
        {
            double p = 12.5;
            decimal v = (decimal)p;

            var mivm = new MovieDetailsViewModel
            {
                Title = "CageMovie",
                Genre = "Drama",
                Length = 122,
                Price = v,
                ImgUrl = "www.google.com"
            };


            _movieRepository.Setup(x => x.GetMovieDetails(4)).Returns(mivm);
            _controller = new MoviesController(_movieRepository.Object);

            var result = _controller.Details(4) as ViewResult;

            Assert.AreEqual(mivm, result.Model);
        }


        [DataTestMethod]
        [DataRow(4, "CageMovie", 144, "nic cage movie", "Crime", 12.5)]
        [DataRow(4, "CageMovie", 144, "nic cage movie", "Crime", 12.5645125)]
        [DataRow(0, "", 0, "", "", 0)]
        public void GetModalMoviesSerializesCorrectly(int id, string title,
            int lm, string d, string g, double p)
        {
            Movy m = new Movy
            {
                movie_id = id,
                movie_title = title,
                length_minutes = (short)lm,
                decription = d,
                genre = g,
                price = (decimal)p,
                release_date = DateTime.Now
            };

            _movieRepository.Setup(x => x.GetModalMovies(1)).Returns(m);
            _controller = new MoviesController(_movieRepository.Object);

            var result = _controller.GetMoviesForModal(1) as JsonResult;
            var jsonSerializer = new JavaScriptSerializer();

            var expected = jsonSerializer.Serialize(m);
            var actual = jsonSerializer.Serialize(result.Data);

            Assert.AreEqual(expected, actual);
        }
    }
}
