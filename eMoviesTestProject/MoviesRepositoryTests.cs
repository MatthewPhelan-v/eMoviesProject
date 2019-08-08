using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using eMovies.Controllers;
using eMovies.Dependencies;
using eMovies.Interfaces;
using eMovies.Models;
using eMovies.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace eMoviesTests
{
    [TestClass]
    public class MoviesRepositoryTests
    {
        private MovieRepository _mr;
        private Movy _m;
        private decimal v;
        private DateTime dt;
        private MovieDetailsViewModel _vm;

        [TestInitialize]
        public void Setup()
        {
            _mr = new MovieRepository();
            double p = 12.5;
            v = (decimal)p;
            dt = DateTime.Now;
            _m = new Movy
            {
                movie_id = 5,
                movie_title = "CageMovie",
                decription = "it is a nick cage movie",
                genre = "RomCom",
                length_minutes = 95,
                price = v,
                release_date = dt,
                image_url = "www.google.com"
            };
            _vm = new MovieDetailsViewModel
            {
                Title = "CageMovie",
                Length = 95,
                Genre = "RomCom",
                Price = v,
                ImgUrl = "www.google.com"
            };
        }

        [TestMethod]
        public void SetupMDVMTest()
        {
            MovieDetailsViewModel sumdvm = _mr.SetupMDVM(_m);

            Assert.AreEqual(_vm.Title, sumdvm.Title);
            Assert.AreEqual(_vm.Length, sumdvm.Length);
            Assert.AreEqual(_vm.Genre, sumdvm.Genre);
            Assert.AreEqual(_vm.Price, sumdvm.Price);
            Assert.AreEqual(_vm.ImgUrl, sumdvm.ImgUrl);

        }

        [TestMethod]
        public void SetupMVMTest()
        {
            MovieViewModel vm = new MovieViewModel
            {
                movie_id = 5,
                movie_title = "CageMovie",
                length_minutes = 95,
                genre = "RomCom",
                price = v,
                image_url = "www.google.com"
            };

            MovieViewModel sumdvm = _mr.SetupMVM(_m);

            Assert.AreEqual(vm.movie_title, sumdvm.movie_title);
            Assert.AreEqual(vm.length_minutes, sumdvm.length_minutes);
            Assert.AreEqual(vm.genre, sumdvm.genre);
            Assert.AreEqual(vm.price, sumdvm.price);
            Assert.AreEqual(vm.image_url, sumdvm.image_url);

        }

        [TestMethod]
        public void SetupMIVMTest()
        {
            List<Movy> m = new List<Movy>
            {
                _m
            };

            MovieViewModel vm = new MovieViewModel
            {
                movie_id = 5,
                movie_title = "CageMovie",
                length_minutes = 95,
                genre = "RomCom",
                price = v,
                image_url = "www.google.com"
            };

            List<MovieViewModel> movies = new List<MovieViewModel>
            {
                vm
            };

            MovieIndexViewModel mivm = new MovieIndexViewModel
            {
                Movies = movies
            };

            MovieIndexViewModel mivm1 = _mr.SetupMIVM(m);

            Assert.AreEqual(mivm.Movies[0].movie_id, mivm1.Movies[0].movie_id);
        }

        [DataTestMethod]
        [DataRow(12, "£12.00")]
        [DataRow(12.5, "£12.50")]
        [DataRow(12.57, "£12.57")]
        [DataRow(12.56542, "£12.57")]
        [DataRow(0, "£0.00")]
        public void AmountFormatsPriceCorrectlyInMVM(double price, string expectedFormattedPrice)
        {
            MovieViewModel mvm = new MovieViewModel
            {
                price = (decimal) price,
            };

            Assert.AreEqual(mvm.formattedAmount, expectedFormattedPrice);
        }

        [TestMethod]
        public void AmountFormatsNullCorrectlyInMVM()
        {
            MovieViewModel mvm5 = new MovieViewModel();

            Assert.AreEqual(mvm5.formattedAmount, "£0.00"); 
        }
    }
}
