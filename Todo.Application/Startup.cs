using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain.Data;
using Todo.Domain.Interfaces;
using Todo.Domain.Models;
using Todo.Domain.Repositories;

namespace Todo.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<TodoDbContext>(opts => {
                opts
                .UseLazyLoadingProxies()
                .UseSqlServer(
                    Configuration["ConnectionStrings:TodoAppConnection"]);
            });

            services.AddScoped<IRepository<TodoItem>, TodoItemRepository>();
            services.AddScoped<IRepository<TodoList>, TodoListRepository>();

            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                SeedData.EnsurePopulated(
                    app.ApplicationServices.CreateScope()
                    .ServiceProvider.GetRequiredService<TodoDbContext>());
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "list",
                    pattern: "List/{id:int}",
                    defaults: new { Controller = "TodoList", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "manipulate-list",
                    pattern: "List/{action}/{id:int?}",
                    defaults: new { Controller = "TodoList" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
