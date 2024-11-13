using Microsoft.AspNetCore.Mvc;
using Project.PL.Areas.Admin.Services;
using Project.PL.Areas.Admin.ViewModels;

namespace Project.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        public IActionResult ChangeOrderStatus(string userId)
        {
            var userOrders = _orderService.GetOrdersByUserId(userId);
            if (userOrders == null || !userOrders.Any())
            {
                return NotFound("No orders found for this user.");
            }

            return View(userOrders);
        }

        [HttpGet]
        public IActionResult EditOrder(int orderId)
        {
            // جلب البيانات الخاصة بالطلب بناءً على OrderId
            var order = _orderService.GetOrderById(orderId);
            if (order == null)
            {
                return NotFound();
            }

            // إرجاع البيانات إلى صفحة العرض (view) لتعديل الطلب
            return View(order);
        }

        [HttpPost]
        public IActionResult EditOrder(int orderId, string userId, string status)
        {
            var order = _orderService.GetOrderById(orderId);

            if (order == null || order.UserId != userId)
            {
                return NotFound("Order not found or mismatched User.");
            }

            order.Status = status;

            var result = _orderService.UpdateOrder(order);

            if (result)
            {
                return RedirectToAction("ChangeOrderStatus", new { userId = userId });
            }
            else
            {
                ModelState.AddModelError("", "Failed to update the order.");
                return View(order);
            }
        }


    }
}
