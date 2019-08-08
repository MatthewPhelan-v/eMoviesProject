using System.Collections.Generic;
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
    public class OrderControllerTests
    {

        private Mock<IOrderRepository> _orderRepository;
        private OrderController _controller;

        [TestInitialize]
        public void Setup()
        {
            _orderRepository = new Mock<IOrderRepository>();
        }

        [TestMethod]
        public void Test()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void OrderIndexActionReturnsCorrectTypeTest()
        {
            ICollection<MovieViewModel> selection = new List<MovieViewModel>();

            _orderRepository.Setup(x => x.GetSelectedMovies(selection, It.IsAny<int>())).Returns(new MovieViewModel[0]);
            _controller = new OrderController(_orderRepository.Object);

            var result = _controller.Index(selection) as ViewResult;
            var modelType = result.Model.GetType();

            Assert.AreEqual(typeof(OrderIndexViewModel), modelType);
        }

        [DataTestMethod]
        [DataRow("12112124", 1211212, 4, 12, true)]
        [DataRow("", 0, 0, 0, false)]
        public void GetOrderJsonSerialIzesCorrectly(string moid, int oid, int mid, int q, bool b)
        {
            var mo = new Movie_Orders
            {
                movieorder_id = moid,
                order_id = oid,
                movie_id = mid,
                quantity = q,
                booked = b
            };

            var orderDetails = new List<Movie_Orders>
            {
                mo
            };

            _orderRepository.Setup(x => x.GetOrder(oid)).Returns(orderDetails);
            _controller = new OrderController(_orderRepository.Object);

            var result = _controller.GetOrder(oid) as JsonResult;
            var jsonSerializer = new JavaScriptSerializer();
            var expected = jsonSerializer.Serialize(orderDetails);
            var actual = jsonSerializer.Serialize(result.Data);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OrderPaymentReturnsCorrectTypeTest()
        {
            PaymentCompletionViewModel pcvm = new PaymentCompletionViewModel();

            _orderRepository.Setup(x => x.Payment(It.IsAny<CardViewModel>(), It.IsAny<int>(), It.IsAny<int>())).Returns(pcvm);
            _controller = new OrderController(_orderRepository.Object);

            var result = _controller.Payment(new CardViewModel()) as ViewResult;

            Assert.AreEqual(pcvm.GetType(), result.Model.GetType());
        }
    }
}
