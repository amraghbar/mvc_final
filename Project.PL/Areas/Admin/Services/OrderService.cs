using Project.DAl.Data;
using Project.PL.Areas.Admin.ViewModels;

namespace Project.PL.Areas.Admin.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<OrderViewModel> GetOrdersByUserId(string userId)
        {
            var orders = _context.Orders
                .Where(o => o.ApplicationUserId == userId)
                .Select(o => new OrderViewModel
                {
                    OrderId = o.OrderId,
                    UserId = o.ApplicationUserId,
                    OrderDate = o.OrderDate,
                    Status = o.OrderStatus
                })
                .ToList();

            return orders;
        }

        public OrderViewModel GetOrderById(int orderId)
        {
            return _context.Orders
                .Where(o => o.OrderId == orderId)
                .Select(o => new OrderViewModel
                {
                    OrderId = o.OrderId,
                    UserId = o.ApplicationUserId,
                    OrderDate = o.OrderDate,
                    Status = o.OrderStatus
                })
                .FirstOrDefault();
        }

        public bool UpdateOrder(OrderViewModel order)
        {
            var existingOrder = _context.Orders.Find(order.OrderId);
            if (existingOrder == null) return false;

            existingOrder.OrderStatus = order.Status;
            existingOrder.OrderDate = order.OrderDate; 

            _context.SaveChanges();
            return true;
        }
    }
}
