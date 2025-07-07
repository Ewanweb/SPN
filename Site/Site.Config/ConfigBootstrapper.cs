using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Site.Application._shared;
using Site.Application._shared.FileUtil.Interfaces;
using Site.Application._shared.FileUtil.Services;
using Site.Application.Agents.AddFeatures;
using Site.Domain.Agents;
using Site.Domain.Repositories;
using Site.Facade.Agents;
using Site.Facade.Projects;
using Site.Infrastructure;
using Site.Infrastructure.Repositories;
using Site.Query;
using Site.Query.Agents.GetById;

namespace Site.Config
{
    public static class ConfigBootstrapper
    {
        public static IServiceCollection Init(IServiceCollection service, IConfiguration config)
        {
            service.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("Default")));


            // Register Mediator
            service.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(AddAgentFeatureCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetAgentByIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(AgentFacade).Assembly);
            });

            service.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/profile/auth/login"; // مسیر لاگین که شما گفتی
                    options.AccessDeniedPath = "/access-denied";
                    options.ExpireTimeSpan = TimeSpan.FromDays(30); // مثلا ۳۰ روز
                    options.Cookie.Name = "Spn.Auth";
                    options.SlidingExpiration = true;
                });

            // Register AutoMapper
            service.AddAutoMapper(typeof(MappingProfile).Assembly);


            // Register HttpContextAccessor
            service.AddHttpContextAccessor();

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
