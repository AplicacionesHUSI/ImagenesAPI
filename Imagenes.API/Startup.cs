using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Imagenes.API.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Imagenes.API.Domain.IRepository;
using Imagenes.API.Persistence.repository;
using Imagenes.API.Domain.IService;
using Imagenes.API.Services;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

namespace Imagenes.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            var sqlstring = Configuration["ConnectionStrings:Dbconnection"];

            //config DB
            services.AddDbContext<SahiDBContext>(options =>
            {
                options.UseSqlServer(sqlstring, sqlOptions => { sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null); });
            });

            //inyecccion 

            services.AddTransient<IServiceDoctor, ServiceDoctor>();
            services.AddTransient<IRepositoryDoctor, RepositoryDoctor>();
            services.AddTransient<IServicePaciente, ServicePaciente>();
            services.AddTransient<IRepositoryPaciente, RepositoryPaciente>();
            services.AddTransient<IServiceHistoriaClinica, ServiceHistoriaClinica>();
            services.AddTransient<IRepositoryElemento, RepositoryElemento>();
            //compresion gzip
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });
            //swagger
            services.AddSwaggerGen(options =>
            {
                // options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new OpenApiInfo() { Title = "Imagenes API" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", "Imagenes API");
            });

            // Set the comments path for the Swagger JSON and UI.


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}