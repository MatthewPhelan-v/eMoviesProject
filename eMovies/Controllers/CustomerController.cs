using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using eMoviesBusinessLogic.Models;
using eMovies.ViewModels;
using eMovies.Models;

namespace eMovies.Controllers
{
    public class CustomerController : Controller
    {

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignIn()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult CustomerDetails()
        {
            CustomerDetailsViewModel vm = new CustomerDetailsViewModel();

            try
            {
                emoviesEntities db = new emoviesEntities();
                Customer customer = db.Customers.SingleOrDefault(x => x.customer_id == 1);

                vm.customer_id = customer.customer_id;
                vm.first_name = customer.first_name;
                vm.last_name = customer.last_name;
                //vm.age = customer.age;
                vm.email = customer.email;
            } catch (Exception e)
            {
                throw e;
            }
            

            return View(vm);
        }

        public ActionResult CustomerOrders()
        {

            DateTime utcDate = DateTime.UtcNow;
            return View();
        }

        [HttpGet]
        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Test(User user)
        {
            return View();
        }
    }
}