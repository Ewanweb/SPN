using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Site.Application._shared.FileUtil.Interfaces;
using Site.Application._shared.FileUtil.Services;
using Site.Domain.Repositories;
using Site.Facade.Agents;
using Site.Facade.Projects;
using Site.Infrastructure;
using Site.Infrastructure.Repositories;

namespace Site.Config
{
    public static class ConfigBootstrapper
    {
        public static IServiceCollection Init(IServiceCollection service, IConfiguration config)
        {
            service.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("Default")));



            service.AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    // تنظیمات مربوط به کاربران
                    options.User.RequireUniqueEmail = true;
                    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                    // تنظیمات مربوط به رمز عبور
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;

                    // تنظیمات مربوط به ورود
                    options.SignIn.RequireConfirmedEmail = true;
                    options.SignIn.RequireConfirmedPhoneNumber = false;

                    // تنظیمات مربوط به قفل شدن کاربر (Lockout)
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // مدت زمان قفل شدن
                    options.Lockout.MaxFailedAccessAttempts = 5; // تعداد دفعات تلاش ناموفق مجاز
                    options.Lockout.AllowedForNewUsers = true;
                }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Register Repositories
            service.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            service.AddScoped<IFileService, FileService>();
            service.AddScoped<IAgentRepository, AgentRepository>();
            service.AddScoped<IProjectRepository, ProjectRepository>();


            // Register Facades
            service.AddScoped<IAgentFacade, AgentFacade>();
            service.AddScoped<IProjectFacade, ProjectFacade>();


            return service;
        }
    }
}
