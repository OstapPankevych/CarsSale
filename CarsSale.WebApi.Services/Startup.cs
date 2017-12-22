using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarsSale.DataAccess.Repositories;
using CarsSale.DataAccess.Repositories.Interfaces;
using CarsSale.DataAccess.Repositories.QueryBuilders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
            ConfigureDataAccessServices(services);
            services.AddMvc();
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
            var b = Configuration.GetSection("ConnectionString")["CarsSaleEntities"];
            var a = Configuration["ConnectionString:CarsSaleEntities"];
            services.AddScoped<IAdvertisementSearchQueryBuilder, AdvertismentSearchQueryBuilder>();
            var provider = services.BuildServiceProvider();
            services.AddScoped<IAdvertisementRepository>(s =>
                new AdvertisementRepository(
                    Configuration["ConnectionString:CarsSaleEntities"],
                    provider.GetService<IAdvertisementSearchQueryBuilder>()));
        }
    }
}
