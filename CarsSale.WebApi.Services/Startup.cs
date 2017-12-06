using CarsSale.DataAccess.Repositories;
using CarsSale.DataAccess.Repositories.Interfaces;
using CarsSale.DataAccess.Repositories.QueryBuilders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarsSale.WebApi.Services
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
            services.AddMvc();
            ConfigureDataAccessServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        private void ConfigureDataAccessServices(IServiceCollection services)
        {
            services.AddScoped<IAdvertisementSearchQueryBuilder, AdvertismentSearchQueryBuilder>();
            var provider = services.BuildServiceProvider();
            services.AddScoped<IAdvertisementRepository>(s =>
                new AdvertisementRepository(
                    Configuration.GetConnectionString("CarsSaleEntities"),
                    provider.GetService<IAdvertisementSearchQueryBuilder>()));
        }
    }
}
