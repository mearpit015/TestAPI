using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestAPI.Model;

namespace TestAPI
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
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("EmployeesDb"));
            services.AddControllers();
            // Swagger
            services.AddSwaggerGen();

            //services.SuppressTelemetryFromPaths(
            //                                    "/api/v1/ping",
            //                                    "/api/v1/warmup",
            //                                    "/health/ready",
            //                                    "/health/live",
            //                                    "/favicon.ico");
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("Policy1",
            //        builder =>
            //        {
            //            builder.WithOrigins("http://localhost:4200/");
            //        });

               
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           // var corsOrigins = Configuration?["ServiceCorsOrigins"].Split(',');

            // CORS (see configuration above)
            app.UseCors(builder =>
            {
                builder
                    .SetIsOriginAllowed(host => true)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithExposedHeaders("Set-Cookie")
                    .WithExposedHeaders("Set-Authentication");

                //if (corsOrigins?.Any() == true)
                //{
                //    builder
                //        .SetIsOriginAllowedToAllowWildcardSubdomains()
                //        .WithOrigins(corsOrigins.ToArray());
                //}
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();


            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseAuthorization();
            
            //user
            //var context = app.ApplicationServices.GetService<ApiContext>();
            //AddTestData(context);
            //app.UseMvc();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

       
    }
}
