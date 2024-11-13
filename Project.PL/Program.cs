using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.DAl.Data;
using Project.PL.Areas.Admin.Services;
using Project.PL.Areas.Admin.ViewModels;
using Project.PL.Areas.Admin.ViewModels.Service;
using Project.PL.Mapping;
using Project_.DAL.Models;
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

        // إعداد خدمات Identity
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultUI()
        .AddDefaultTokenProviders();

        // إضافة خدمات MVC و Razor Pages
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        // إعداد AutoMapper
        builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));

        // إعداد الجلسة
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30); // مدة انتهاء الجلسة
            options.Cookie.HttpOnly = true; // جعل الكوكيز غير متاحة للجافاسكريبت
            options.Cookie.IsEssential = true; // الضرورة لجعل الكوكيز متاحة للطلبات
        });

        builder.Services.AddScoped<IOrderService, OrderService>();

        var app = builder.Build();

        // تكوين Pipeline معالجة الطلبات
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage(); // لعرض معلومات الأخطاء في حالة التطوير
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

        // إضافة الجلسة في Pipeline معالجة الطلبات
        app.UseSession();

        // تسجيل المسارات
        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Users}/{action=Index}/{id?}");

        app.MapRazorPages(); // لجميع صفحات Razor الخاصة بك

        app.Run();
    }
}
