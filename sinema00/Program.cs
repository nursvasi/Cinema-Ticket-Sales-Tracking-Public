using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using sinema00.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
AddCookie(option =>
{
    option.LoginPath = "/Startp/Login";
    option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
});//2.ad�m
builder.Services.AddDbContext<sinema00Context>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("dbconstring")));
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
app.UseAuthentication();//3.ad�m
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Startp}/{action=Login}/{id?}");//4.ad�m

app.Run();
