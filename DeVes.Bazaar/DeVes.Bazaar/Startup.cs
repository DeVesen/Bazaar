using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using DeVes.Bazaar.Data.MongoDb;
using DeVes.Bazaar.Interfaces;
using DeVes.Bazaar.Logic;
using Microsoft.OpenApi.Models;

namespace DeVes.Bazaar
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            configuration.SetInternalsToConfiguration(env);

            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMongoDbRepository(_configuration["Data:ConnectionString"], "DeVesBazaarDb");

            services.AddTransient<IManufacturerLogic, ManufacturerLogic>();
            services.AddTransient<ICategoryLogic, CategoryLogic>();
            services.AddTransient<ISellerLogic, SellerLogic>();
            services.AddTransient<IArticleLogic, ArticleLogic>();

            services.AddTransient<ISaleLogic, SaleLogic>();
            services.AddTransient<IAccountingLogic, AccountingLogic>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DeVesen.Bazaar API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.InitializeRepositories();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DeVesen.Bazaar API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }



    }

    public static class StartupExtensions
    {
        public static void SetInternalsToConfiguration(this IConfiguration configuration, IWebHostEnvironment env)
        {
            configuration["DataRootPath"] = Path.Combine(env.ContentRootPath, @"app-data");
        }

        public static void InitializeRepositories(this IApplicationBuilder app)
        {
            var categoryLogic = app.ApplicationServices.GetRequiredService<ICategoryLogic>();
            categoryLogic.BasicInitialization();

            var manufacturerLogic = app.ApplicationServices.GetRequiredService<IManufacturerLogic>();
            manufacturerLogic.BasicInitialization();
        }
    }
}
