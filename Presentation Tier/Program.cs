using Business_Logic_Tier.Interfaces;
using Business_Logic_Tier.Repositories;
using Data_Access_Tier.Context;
using Data_Access_Tier.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presentation_Tier.Mapper;

namespace Presentation_Tier
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<MVCAppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(map => map.AddProfile(new MappingProfiles()));

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Home/Error");
                });

            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.SignIn.RequireConfirmedAccount = false;

            })
            .AddEntityFrameworkStores<MVCAppDbContext>()
            .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=SignUp}/{id?}");

            app.Run();
        }
    }
}