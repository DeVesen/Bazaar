using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using DeVes.Bazaar.Contracts.Logic;
using DeVes.Bazaar.Contracts.Repositories;
using DeVes.Bazaar.Data.JsonDb;
using DeVes.Bazaar.GraphQl;
using DeVes.Bazaar.GraphQl.Query;
using DeVes.Bazaar.GraphQl.Schemas;
using DeVes.Bazaar.GraphQl.Types;
using DeVes.Bazaar.Logic;
using GraphQL;
using GraphQL.NewtonsoftJson;
using Microsoft.OpenApi.Models;

namespace DeVes.Bazaar
{
    public class Startup
    {
        private readonly RepoOptions _basicDataRepoOptions;
        private readonly RepoOptions _sellerDataRepoOptions;

        public Startup(IHostEnvironment env)
        {
            var appDataPath = Path.Combine(env.ContentRootPath, @"app-data");

            if (Directory.Exists(appDataPath) is false)
            {
                Directory.CreateDirectory(appDataPath);
            }

            _basicDataRepoOptions  = new RepoOptions(Path.Combine(appDataPath, @"BasicData.json"));
            _sellerDataRepoOptions = new RepoOptions(Path.Combine(appDataPath, @"SellerData.json"));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IManufacturerRepository>(provider =>
                                                               new ManufacturerRepository(_basicDataRepoOptions));
            services.AddTransient<ICategoryRepository>(provider => new CategoryRepository(_basicDataRepoOptions));
            services.AddTransient<ISellerRepository>(provider => new SellerRepository(_sellerDataRepoOptions));
            services.AddTransient<IArticleRepository>(provider => new ArticleRepository(_sellerDataRepoOptions));

            services.AddTransient<IManufacturerLogic, ManufacturerLogic>();
            services.AddTransient<ICategoryLogic, CategoryLogic>();
            services.AddTransient<ISellerLogic, SellerLogic>();
            services.AddTransient<IArticleLogic, ArticleLogic>();

            services.AddTransient<GraphQlLogic>();
            services.AddTransient<IDocumentExecuter, DocumentExecuter>();
            services.AddTransient<IDocumentWriter, DocumentWriter>();
            services.AddTransient<SellerModelType>();
            services.AddTransient<ArticleModelType>();
            services.AddTransient<CategoryModelType>();
            services.AddTransient<ManufacturerModelType>();
            services.AddTransient<GlobalAppQuery>();
            services.AddTransient<GlobalAppSchema>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                             new OpenApiInfo {Title = "DeVesen.Bazaar API", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ApplicationServices.GetRequiredService<ICategoryLogic>().BasicInitializationAsync().GetAwaiter()
               .GetResult();
            app.ApplicationServices.GetRequiredService<IManufacturerLogic>().BasicInitializationAsync().GetAwaiter()
               .GetResult();

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "DeVesen.Bazaar API V1"); });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}