using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using ThanhThoaiRestaurant.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using ThanhThoaiRestaurant.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using MyPhamCheilinus.Hubs;





var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<QuanLyNhaHangContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("ThanhThoaiRestaurant")));

builder.Services.AddScoped<IVnPayService, VnPayService>();

builder.Services.AddScoped<IPayPalService, PayPalService>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
//add dependency inject cho MailService
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddSignalR();
builder.Services.AddScoped<IShippingService, ShippingService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    options.SaveTokens = true;

    options.Events.OnCreatingTicket = ctx =>
    {
        var picture = ctx.User.GetProperty("picture").GetString();
        if (!string.IsNullOrEmpty(picture))
        {
            var identity = (ClaimsIdentity)ctx.Principal.Identity;
            identity.AddClaim(new Claim("urn:google:picture", picture));
        }
        return Task.CompletedTask;
    };
})
.AddFacebook(options =>
{
    options.AppId = "691482856264629";
    options.AppSecret = "99c3f0f20aba95e51cbe35517551bf34";
});

builder.Services.AddRazorPages();

/* builder.Services.AddScoped<IGioHangService, GioHangService>(); */

builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddRazorPages().AddViewOptions(options =>
{
    options.HtmlHelperOptions.ClientValidationEnabled = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();



app.UseAuthentication();

app.UseAuthorization();
app.MapHub<ConnectedHub>("/ConnectedHub");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapAreaControllerRoute(
        name: "productDetails",
        areaName: "Admin", // Tên của Area
        pattern: "Admin/AdminMenu/Details/{id}", // Đường dẫn của trang chi tiết
        defaults: new { controller = "AdminMenu", action = "Details" }
        );

    endpoints.MapAreaControllerRoute(
        name: "productEdit",
        areaName: "Admin", // Tên của Area
        pattern: "Admin/AdminMenu/Edit/{id}", // Đường dẫn của trang chi tiết
        defaults: new { controller = "AdminMenu", action = "Edit" }
        );

    endpoints.MapAreaControllerRoute(
       name: "productCreate",
       areaName: "Admin", // Tên của Area
       pattern: "Admin/AdminMenu/Create/{id}", // Đường dẫn của trang chi tiết
       defaults: new { controller = "AdminMenu", action = "Create" }
       );

    endpoints.MapAreaControllerRoute(
      name: "orderDetails",
      areaName: "Admin", // Tên của Area
      pattern: "Admin/AdminDonHang/Details/{id}", // Đường dẫn của trang chi tiết
      defaults: new { controller = "AdminDonHang", action = "Details" }
      );

    endpoints.MapAreaControllerRoute(
      name: "billDetails",
      areaName: "Admin", // Tên của Area
      pattern: "Admin/AdminHoaDon/Details/{id}", // Đường dẫn của trang chi tiết
      defaults: new { controller = "AdminHoaDon", action = "Details" }
      );
});



app.MapControllerRoute(
    name: "orders1",
    pattern: "{area:exists}/{controller=AdminDonHang}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "orders1",
    pattern: "{area:exists}/{controller=AdminThongKe}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "orders1",
    pattern: "{area:exists}/{controller=AdminHoaDon}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "login",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "register",
    pattern: "{controller=Account}/{action=Register}/{id?}");

app.MapControllerRoute(
    name: "information",
    pattern: "{controller=Account}/{action=Information}/{id?}");

app.MapControllerRoute(
    name: "productDetails",
    pattern: "Product/Details/{id}",
    defaults: new { controller = "Menu", action = "Details" });

app.MapControllerRoute(
    name: "default1",
    pattern: "{controller=GioHang}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "bills",
    pattern: "{controller=HoaDon}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "orders1",
    pattern: "{controller=DonHang}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "orders2",
    pattern: "{controller=DonHang}/{action=ChoXacNhan}/{id?}");

app.MapControllerRoute(
    name: "orders2",
    pattern: "{controller=DonHang}/{action=DangGiao}/{id?}");

app.MapControllerRoute(
    name: "orders2",
    pattern: "{controller=DonHang}/{action=DaGiao}/{id?}");

app.MapControllerRoute(
    name: "orders2",
    pattern: "{controller=DonHang}/{action=Huy}/{id?}");


app.MapControllerRoute(
    name: "ordertDetails",
    pattern: "DonHang/Details/{id}",
    defaults: new { controller = "DonHang", action = "Details" });

app.MapControllerRoute(
        name: "api",
        pattern: "api/{controller=GiamGia}/{action=CheckCoupon}/{id?}");


app.MapRazorPages();



app.Run();


  
   






