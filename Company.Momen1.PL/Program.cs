using Company.Momen1.BLL.Interfaces;
using Company.Momen1.BLL.Repositories;
using Company.Momen1.DAL.Data.Contexts;
using Company.Momen1.PL.Controllers;
using Company.Momen1.PL.DTO.Mapping;
using Company.Momen1.PL.DTO.Services;
using Microsoft.EntityFrameworkCore;

namespace Company.Momen1.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(); //Register Built-in MVC Services

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepositories>();   //Allow DI For DpartmentRepositores
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); //Allow DI For EmployeeRepository
            builder.Services.AddDbContext<CompanyDbContext>(options=>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefultConnection"));
            }); //Allow Di For CompanyDbContext

            //builder.Services.AddAutoMapper(typeof(EmployeePropfile));
            builder.Services.AddAutoMapper(M=> M.AddProfile(new EmployeePropfile()));

            //life time 
            //builder.Services.AddScoped(); //Create object Life Time per requset
            //builder.Services.AddTransient(); 
            //builder.Services.AddSignalRCore

            builder.Services.AddScoped<IScopedService, ScopedService>();
            builder.Services.AddTransient<ITransentService, TransentService>();
            builder.Services.AddSingleton<ISingletonService, SingleService>();

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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
