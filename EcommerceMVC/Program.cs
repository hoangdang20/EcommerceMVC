using EcommerceMVC.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add session
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(option =>
{
	option.IdleTimeout = TimeSpan.FromMinutes(30);
	option.Cookie.HttpOnly = true;
	option.Cookie.IsEssential = true; 
});

// Add DbContext
builder.Services.AddDbContext<Hshop2023Context>(option=> {
    option.UseSqlServer(builder.Configuration.GetConnectionString("connString"));
});
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

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
