using Project.PL.Areas.Admin.ViewModels;

namespace Project.PL.Areas.Admin.Services
{
    public interface IOrderService
    {
        IEnumerable<OrderViewModel> GetOrdersByUserId(string userId);
        OrderViewModel GetOrderById(int orderId); 
        bool UpdateOrder(OrderViewModel order); 
    }
}
