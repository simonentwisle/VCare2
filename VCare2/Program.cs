using Microsoft.EntityFrameworkCore;
using VCare2.DatabaseLayer;
using VCare2.RepositoryLayer;
using VCare2.ServiceLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CareHomeContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("CareHomeContext")));

builder.Services.AddTransient(typeof(StaffService), typeof(StaffService));
builder.Services.AddTransient(typeof(StaffRepository), typeof(StaffRepository));
builder.Services.AddTransient(typeof(StatisticsService), typeof(StatisticsService));
builder.Services.AddTransient(typeof(StateService), typeof(StateService));
builder.Services.AddTransient(typeof(StaffInjectionViewService), typeof(StaffInjectionViewService));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

