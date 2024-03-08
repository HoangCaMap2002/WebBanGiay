using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebBanGiay.Models;
using WebBanGiay.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.LoginPath = "/Access/Login";
    option.ReturnUrlParameter = "urlRedirect";
    option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("CustomerPolicy", policy => policy.RequireRole("Customer"));
});
var connectionString = builder.Configuration.GetConnectionString("QlbanGiayContext");
builder.Services.AddDbContext<QlbanGiayContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(connectionString));
    options.UseLazyLoadingProxies(); // Kích hoạt tải lười biếng
});
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseCookiePolicy();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
            name: "area",
            pattern: "{area:exists}/{controller=HomeAdmin}/{action=Index}/{id?}");
app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
