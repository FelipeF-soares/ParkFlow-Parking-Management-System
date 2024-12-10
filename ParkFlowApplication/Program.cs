using Microsoft.EntityFrameworkCore;
using ParkFlowData.DataBaseContext;
using ParkFlowData.Repository;
using ParkFlowData.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string stringConnection = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<ParkFlowContext>
    (
        option => option.UseMySql(stringConnection,ServerVersion.AutoDetect(stringConnection))
    );

builder.Services.AddScoped<IVehiclePersist, VehiclePersist>();
builder.Services.AddScoped<IEntryExitAccessPersist, EntryExitAccessPersist>();
builder.Services.AddScoped (typeof(IGenericPersist<>), typeof(GenericPersist<>));

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
    pattern: "{area=User}/{controller=Home}/{action=Index}/{id?}");

app.Run();
