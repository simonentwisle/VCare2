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

builder.Services.AddTransient(typeof(StaffQualificationService), typeof(StaffQualificationService));
builder.Services.AddTransient(typeof(StaffQualificationRepository), typeof(StaffQualificationRepository));

builder.Services.AddTransient(typeof(JobTitleService), typeof(JobTitleService));
builder.Services.AddTransient(typeof(JobTitleRepository), typeof(JobTitleRepository));


builder.Services.AddTransient(typeof(LocationService), typeof(LocationService));
builder.Services.AddTransient(typeof(LocationRepository), typeof(LocationRepository));

builder.Services.AddTransient(typeof(QualificationService), typeof(QualificationService));
builder.Services.AddTransient(typeof(QualificationRepository), typeof(QualificationRepository));

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

