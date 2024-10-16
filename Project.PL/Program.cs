using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.DAl.Data;
using Project.PL.Models;
using Project.PL.Mapping;
using System.Reflection;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// إعداد سلسلة الاتصال بقاعدة البيانات
		var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
			?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
		builder.Services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(connectionString));

		builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
					.AddEntityFrameworkStores<ApplicationDbContext>();
		builder.Services.AddControllersWithViews();


		// إضافة خدمات MVC و Razor Pages
		builder.Services.AddControllersWithViews();
		builder.Services.AddRazorPages(); // إضافة دعم صفحات Razor

		// إعداد AutoMapper إذا كنت تستخدمه
		builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));

		var app = builder.Build();

		// تكوين Pipeline معالجة الطلبات
		if (app.Environment.IsDevelopment())
		{
			app.UseMigrationsEndPoint();
		}
		else
		{
			app.UseExceptionHandler("/Home/Error");
			app.UseHsts(); // القيمة الافتراضية لـ HSTS هي 30 يومًا
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseAuthentication(); // تأكد من وجود هذا السطر
		app.UseAuthorization();

		// تسجيل المسارات
		app.MapControllerRoute(
			name: "areas",
			pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");

		app.MapRazorPages(); // لجميع صفحات Razor الخاصة بك

		app.Run();
	}
}
