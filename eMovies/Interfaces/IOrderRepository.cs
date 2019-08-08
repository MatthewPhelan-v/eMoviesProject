using eMovies.Models;
using eMovies.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMovies.Interfaces
{
    public interface IOrderRepository
    {
        ICollection<MovieViewModel> GetSelectedMovies(ICollection<MovieViewModel> selection, int orderId);//
        PaymentCompletionViewModel Payment(CardViewModel cardDetails, int customerId, int orderId);
        List<Movie_Orders> GetOrder(int id);///
    }
}
