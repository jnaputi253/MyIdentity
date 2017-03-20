using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyIdentity.Data;
using MyIdentity.Models;

/*
 * TODO: I want you to continue learning about Identity in Core.
 */

namespace MyIdentity
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment environment)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppIdentityDbContext>(ConfigureConnectionString);
            services.AddIdentity<AppUser, IdentityRole>(ConfigureCredentialSettings)
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            services.AddTransient<IPasswordValidator<AppUser>, PasswordValidator<AppUser>>();

            services.AddMvc();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseIdentity();
            app.UseMvcWithDefaultRoute();
        }

        private void ConfigureConnectionString(DbContextOptionsBuilder dbContextOptions)
        {
            dbContextOptions.UseSqlServer(Configuration.GetConnectionString("SportsStoreConnection"));
        }

        private void ConfigureCredentialSettings(IdentityOptions options)
        {
            options.Password.RequiredLength = 3;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
        }
    }
}
