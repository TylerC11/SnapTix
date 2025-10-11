using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SnapTix.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SnapTixContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SnapTixContext") ?? throw new InvalidOperationException("Connection string 'SnapTixContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.UseStaticFiles();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
