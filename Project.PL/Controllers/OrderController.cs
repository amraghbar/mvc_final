using Project_.DAL.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.DAl.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.PL.ViewModel;

namespace Project_.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        public OrderController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            this.userManager = userManager;

            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            // الحصول على المستخدم الحالي
            var currentUser = await userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Accounts");
            }

            // استرجاع الطلبات الخاصة بالمستخدم
            var orders = await _context.Orders
                .Include(o => o.OrderItem)
                .ThenInclude(oi => oi.ProductBase)
                .Where(o => o.ApplicationUserId == currentUser.Id)
                .OrderByDescending(o => o.OrderDate) // ترتيب الطلبات من الأحدث إلى الأقدم
                .ToListAsync();

            return View(orders);
        }


        // إجراء لإنشاء طلب جديد بناءً على محتويات السلة
        public async Task<IActionResult> CreateOrder(OrderDetails orderDetails)
        {
            // استرجاع معرف المستخدم الحالي
            var currentUser = await userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Accounts");
            }

            // استرجاع عناصر السلة الخاصة بالمستخدم
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.ApplicationUserId == currentUser.Id);

            if (cart == null || !cart.CartItems.Any())
            {
                // إذا كانت السلة فارغة، العودة إلى الصفحة مع رسالة تحذيرية
                TempData["Message"] = "Your cart is empty!";
                return RedirectToAction("Index", "Cart");
            }

            // حساب الإجمالي الكلي للطلب
            decimal totalAmount = cart.CartItems.Sum(item =>
            {
                decimal price = item.Product switch
                {
                    Featured fp => decimal.TryParse(fp.Af_Price, out var featuredPrice) ? featuredPrice : 0,
                    NewProd np => decimal.TryParse(np.Af_Price, out var newProdPrice) ? newProdPrice : 0,
                    Inspired ip => decimal.TryParse(ip.Af_Price, out var inspiredPrice) ? inspiredPrice : 0,
                    Item ip => decimal.TryParse(ip.Af_Price, out var Items) ? Items : 0,
                    _ => item.Product.Price
                };
                return price * item.Quantity;
            });

            // إنشاء الطلب الجديد باستخدام البيانات المدخلة
            var order = new Order
            {
                ApplicationUserId = currentUser.Id,
                OrderDate = DateTime.Now,
                OrderStatus = "Pending",
                TotalAmount = totalAmount,
                Address = orderDetails.Address, // استخدام العنوان المدخل
                City = orderDetails.City,       // استخدام المدينة المدخلة
                mobile = orderDetails.PhoneNumber, // استخدام رقم الهاتف المدخل
                OrderItem = cart.CartItems.Select(ci => new OrderItem
                {
                    ProductBaseId = ci.ProductId,
                    Quantity = ci.Quantity,
                    Price = ci.Product.Price
                }).ToList()
            };

            // إضافة الطلب إلى قاعدة البيانات
            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();


            // إعادة توجيه المستخدم إلى صفحة تأكيد الطلب أو الصفحة الرئيسية
            TempData["Message"] = "Order has been created successfully!";
            return RedirectToAction("Profile", "User");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            // حذف الطلب
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            // إعادة التوجيه إلى قائمة الطلبات
            return RedirectToAction("Profile", "User");
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItem)
                .ThenInclude(oi => oi.ProductBase)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }


    }
}