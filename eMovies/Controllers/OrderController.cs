using eMovies.Interfaces;
using eMovies.Models;
using eMovies.Dependencies;
using eMovies.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eMovies.Controllers
{

    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private int orderId;
        private int customerId;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;

        }
        
        public int OrderIdGenerator()
        {
            Random random = new Random();
            orderId = random.Next(1, 1000000);

            return orderId;
        }

        public int CustomerIdGenerator()
        {
            Random random = new Random();
            customerId = random.Next(1, 1000000);
            
            return customerId;
        }

        public ActionResult Index(ICollection<MovieViewModel> selection)
        {
            int orderId = OrderIdGenerator();
            ICollection<MovieViewModel> nextPage = _orderRepository.GetSelectedMovies(selection, orderId);

            OrderIndexViewModel oivm = new OrderIndexViewModel
            {
                orderId = orderId,
                selection = nextPage
            };

            return View(oivm);
        }

        public ActionResult MakeOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Payment(CardViewModel cardDetails)
        {
            int oid = 0;
            try
            {
                oid = (int)Session["OrderId"];
            } catch
            {

            }
            
            int cid = CustomerIdGenerator();

            PaymentCompletionViewModel pcvm = _orderRepository.Payment(cardDetails, cid, oid);

            ViewBag.CustomerId = cid;
            return View(pcvm);
        }

        public JsonResult GetOrder(int id)
        {
            List<Movie_Orders> orderDetails= _orderRepository.GetOrder(id);
            return Json(orderDetails);
        }
    }
}