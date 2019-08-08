using System.Collections.Generic;
using eMovies.Dependencies;
using eMovies.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eMoviesTests
{
    [TestClass]
    public class OrderRepositoryTests
    {

        private OrderRepository _or;
        private decimal v;

        [TestInitialize]
        public void Setup()
        {
            _or = new OrderRepository();
            double p = 12.5;
            v = (decimal)p;
        }

        [DataTestMethod]
        [DataRow(12, 0, 1)]
        [DataRow(11, 11, 2)]
        [DataRow(0,12,1)]
        [DataRow(0,0,0)]
        public void RemoveMoviesWithZeroQuantityForListTest(int q1, int q2, int count)
        {
            
            MovieViewModel mvm = new MovieViewModel
            {
                quantity = q1
            };

            MovieViewModel mvm1 = new MovieViewModel
            {
                quantity = q2
            };

            ICollection<MovieViewModel> selection = new List<MovieViewModel>
            {
                mvm,
                mvm1
            };

            Assert.AreEqual(2, selection.Count);
            List<MovieViewModel> unselectedRemoved = _or.RemoveUnselected(selection);
            Assert.AreEqual(count, unselectedRemoved.Count);
        }

        [DataTestMethod]
        [DataRow(1, 1, "11")]
        [DataRow(0, 0, "00")]
        [DataRow(999999, 1, "9999991")]
        [DataRow(1, 999999, "1999999")]
        [DataRow(999999, 999999, "999999999999")]
        public void MovieOrderIdConcatenationTest(int oid, int mid, string expected)
        {
            var result = _or.ConcatenateMOId(oid, mid);
            Assert.AreEqual(result, expected);
        }
    }
}
