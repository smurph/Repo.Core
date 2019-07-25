using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Repo.Core.Storage.Mongo;
using Repo.Core.WebApi.Middleware;

namespace Repo.Core.WebApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseMiddleware<ExceptionMiddleware>();

                //app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            MongoConnectionSettings mongoSettings = GetMongoSettings();
            var mongoClient = new MongoClient(mongoSettings.ConnectionString);
            var mongoContext = new MongoContext(mongoClient, mongoSettings);

            builder.Register(c => new PersonRepository(mongoContext)).AsImplementedInterfaces();
        }

        private MongoConnectionSettings GetMongoSettings()
        {
            return new MongoConnectionSettings()
            {
                DatabaseName = Configuration["Mongo:DatabaseName"],
                ConnectionString = Configuration["Mongo:ConnectionString"],
                PersonCollectionName = Configuration["Mongo:PeopleCollectionName"]
            };
        }
    }
}
