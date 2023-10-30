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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<QuanLyNhaHangContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("ThanhThoaiRestaurant")));



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
});

app.MapControllerRoute(
    name: "products",
    pattern: "{area:exists}/{controller=AdminMenu}/{action=Index}/{id?}");



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


app.MapRazorPages();



app.Run();


  
   






