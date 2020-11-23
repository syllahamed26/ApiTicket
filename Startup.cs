using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiTckets.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace apiTckets
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
            services.AddControllers();
            services.AddDbContext<ApiTicketDbContext>(o => o.UseMySql(Configuration.GetConnectionString("ConnexionDb")));
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Api vente de tickets en ligne",
                    Description = "Documentation de mon api de vente de tickets en ligne avec ASP.NET Core",
                    TermsOfService = new Uri("https://shamedpro.tk"),
                    Contact = new OpenApiContact
                    {
                        Name = "Hamed Cédric SYLLA",
                        Email = "syllahamed26@yahoo.com",
                        Url = new Uri("https://shamedpro.tk")
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Documentation de mon API de Vente de tickets en ligne");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
