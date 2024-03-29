using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using DeVes.Bazaar.Contracts;
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
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

namespace DeVes.Bazaar
{
    public class Startup
    {
        private readonly RepoOptions _basicDataRepoOptions;
        private readonly RepoOptions _sellerDataRepoOptions;


        public Startup(IHostEnvironment env, IConfiguration configuration)
        {
            var appDataPath = InitializeLocalFileEnvironment(env);

            _basicDataRepoOptions  = new RepoOptions(Path.Combine(appDataPath, @"BasicData.json"));
            _sellerDataRepoOptions = new RepoOptions(Path.Combine(appDataPath, @"SellerData.json"));

            ConfigureTracing(configuration);
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ITempDataMemory>(provider =>
            {
                var result = new TempDataMemory();
                result.Set("manufacturer", Guid.NewGuid());
                result.Set("manufacturer.create", Guid.NewGuid());
                result.Set("manufacturer.update", Guid.NewGuid());
                result.Set("category", Guid.NewGuid());
                result.Set("category.create", Guid.NewGuid());
                result.Set("category.update", Guid.NewGuid());
                result.Set("seller", Guid.NewGuid());
                result.Set("seller.create", Guid.NewGuid());
                result.Set("seller.update", Guid.NewGuid());
                result.Set("article", Guid.NewGuid());
                result.Set("article.create", Guid.NewGuid());
                result.Set("article.update", Guid.NewGuid());
                return result;
            });

            services.AddTransient<IManufacturerRepository>(provider => new ManufacturerRepository(_basicDataRepoOptions));
            services.AddTransient<ICategoryRepository>(provider => new CategoryRepository(_basicDataRepoOptions));
            services.AddTransient<ISellerRepository>(provider => new SellerRepository(_sellerDataRepoOptions));
            services.AddTransient<IArticleRepository>(provider => new ArticleRepository(_sellerDataRepoOptions));

            services.AddTransient<IManufacturerLogic, ManufacturerLogic>();
            services.AddTransient<ICategoryLogic, CategoryLogic>();
            services.AddTransient<ISellerLogic, SellerLogic>();
            services.AddTransient<IArticleLogic, ArticleLogic>();

            services.AddTransient<ISaleLogic, SaleLogic>();
            services.AddTransient<ISellerBillingLogic, SellerBillingLogic>();

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


        private static string InitializeLocalFileEnvironment(IHostEnvironment env)
        {
            var appDataPath = Path.Combine(env.ContentRootPath, @"app-data");

            if (Directory.Exists(appDataPath) is false)
            {
                Directory.CreateDirectory(appDataPath);
            }

            return appDataPath;
        }

        private static void ConfigureTracing(IConfiguration configuration)
        {
            var cfgDefaultValue = configuration.GetSection("Logging")
                                               ?.GetSection("LogLevel")
                                               ?.GetValue<string>("Default") ?? "Information";
            var cfgConsoleValue = configuration.GetSection("Logging")
                                               ?.GetSection("LogLevel")
                                               ?.GetValue<string>("Console") ?? cfgDefaultValue;

            if (Enum.TryParse(cfgConsoleValue, out SourceLevels sourceTraceLevelConsole) is false) return;
            if (sourceTraceLevelConsole == SourceLevels.Off) return;

            Trace.Listeners.Add(new TraceListener.ConsoleTraceListener
                                    { Filter = new EventTypeFilter(sourceTraceLevelConsole) });
        }
    }
}