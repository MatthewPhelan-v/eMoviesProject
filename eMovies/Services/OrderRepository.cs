using eMovies.Interfaces;
using eMovies.Models;
using eMovies.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eMovies.Dependencies
{

    public class OrderRepository : IOrderRepository
    {
        private readonly emoviesEntities _db;
        public OrderRepository()
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

        public ICollection<MovieViewModel> GetSelectedMovies(ICollection<MovieViewModel> selection, int orderId)
        {
            List<MovieViewModel> nextPage = RemoveUnselected(selection);

            foreach (MovieViewModel s in nextPage)
            {
                PassMODetails(s, orderId);
            }

            return nextPage;
        }

        public List<MovieViewModel> RemoveUnselected(ICollection<MovieViewModel> selection)
        {
            List<MovieViewModel> nextPage = new List<MovieViewModel>();

            foreach (MovieViewModel s in selection)
            {
                if (s.quantity > 0)
                {
                    nextPage.Add(s);
                }
            }

            return nextPage;
        }

        private void PassMODetails(MovieViewModel s, int orderId)
        {
            int movieId = s.movie_id;
            string movieorderId = ConcatenateMOId(orderId, movieId);

            Movie_Orders mo = new Movie_Orders
            {
                movieorder_id = movieorderId,
                order_id = orderId,
                movie_id = s.movie_id,
                quantity = s.quantity
            };

            _db.Movie_Orders.Add(mo);
            _db.SaveChanges();
        }

        public string ConcatenateMOId(int oid, int mid)
        {
            return String.Concat(oid, mid);
        }

        public PaymentCompletionViewModel Payment(CardViewModel cardDetails, int cid, int oid)
        {
            PassCustomerDetails(cid, cardDetails.card_name);
            passCardDetails(cid, cardDetails);
            passOrderDetails(cid, cardDetails.total_price, oid);
            List<int> movieIds = cardDetails.movie_id;

            UpdateMODetails(oid, movieIds);

            _db.SaveChanges();

            PaymentCompletionViewModel pcvm = SetUpOrderVM(oid, cardDetails);

            return pcvm;
        }

        private PaymentCompletionViewModel SetUpOrderVM(int orderId, CardViewModel cardDetails)
        {
            //init view model(vm) and Movie order list for vm.
            PaymentCompletionViewModel pcvm = new PaymentCompletionViewModel();
            List<OrderViewModel> movieOrdersList = new List<OrderViewModel>();
            //list of movie_orders with order_id and booked, to get movie_ids and quantities.
            List<Movie_Orders> movieOrders =_db.Movie_Orders.Where(b => b.order_id == orderId && b.booked == true).ToList();
            //initalise total cost to keep track of how much the customer has spent.
            decimal totalCost = 0;
            pcvm.name = cardDetails.card_name;
            //set customer's card number from details sent over from payment page.
            pcvm.card_number = cardDetails.card_number;

            //for each movie booked
            foreach (Movie_Orders mo in movieOrders)
            {
                //init nested view model for the list of movies bought from.
                OrderViewModel ovm = new OrderViewModel();
                //get quantity of tickets booked
                int quantity = mo.quantity;
                //get record from Movie table with the same id.
                Movy movie =_db.Movies.First(m => m.movie_id == mo.movie_id);
                //get the movie title and price.
                string movieName = movie.movie_title;
                decimal moviePrice = movie.price;
                //add details to nested view model.
                ovm.movie_title = movieName;
                ovm.price = moviePrice;
                ovm.quantity = quantity;
                movieOrdersList.Add(ovm);
                //add the total price spent the total cost
                //i.e. price of the ticket * the number bought.
                totalCost += (moviePrice * quantity);
            }
            //add total cost to view model.
            pcvm.total_cost = totalCost;
            pcvm.movies_orders = movieOrdersList;

            return pcvm;
        }

        private void PassCustomerDetails(int cid, string customer_name)
        {
            Customer c = new Customer
            {
                customer_id = cid,
                first_name = customer_name
            };

            _db.Customers.Add(c);
        }
        
        private void passCardDetails(int cid, CardViewModel cardDetails)
        {
            CardDetail cd = new CardDetail();

            if (_db.CardDetails.Find(cardDetails.card_number) == null)
            {
                cd.customer_id = cid;
                cd.card_number = cardDetails.card_number;
                cd.exp_month = cardDetails.expiry_month;
                cd.exp_year = cardDetails.expiry_year;
                cd.cvv = cardDetails.cvv;

               _db.CardDetails.Add(cd);
            }
        }

        private void passOrderDetails(int customer_id, decimal cost, int oid)
        {
            DateTime dt = DateTime.Now;
            Order o = new Order
            {
                order_id = oid,
                customer_id = customer_id,
                cost = cost,
                order_date = dt
            };
        }

        private void UpdateMODetails(int orderId, List<int> movieIds)
        {
            foreach (int movieId in movieIds)
            {
                string MOId = String.Concat(orderId, movieId);
               _db.Movie_Orders.Single(a => a.movieorder_id == MOId).booked = true;
            }

        }

        public List<Movie_Orders> GetOrder(int id)
        {
            return _db.Movie_Orders.Where(x => x.order_id == id).ToList();
        }

    }
}