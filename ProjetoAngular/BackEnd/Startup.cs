using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using BackEnd.Data;
using BackEnd.Data.Repository.Interfaces;
using BackEnd.Data.Services;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace BackEnd
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

            services.AddDbContext<DataContext>(
                opcoes => opcoes.UseSqlServer(
                    Configuration.GetConnectionString("ConexaoBaseDados")
                )
            ); 

            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IAdministradorRepository, AdministradorRepository>();
            services.AddScoped<IAutorRepository, AutorRepository>();
            services.AddScoped<IClassificacaoRepository, ClassificacaoRepository>();
            services.AddScoped<IGeneroRepository, GeneroRepository>();
            services.AddScoped<IMarcaRepository, MarcaRepository>();
            services.AddScoped<ISegmentoRepository, SegmentoRepository>();
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<ICompraItemRepository, CompraItemRepository>();
            services.AddScoped<ICompraRepository, CompraRepository>();



            services.AddControllers()
                    .AddNewtonsoftJson(
                        opt => opt.SerializerSettings.ReferenceLoopHandling = 
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BackEnd", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackEnd v1"));
            }

            

            app.UseRouting();

            app.UseCors(
                c => c.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()  
            );
            
            app.UseStaticFiles(new StaticFileOptions(){
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
                RequestPath = new PathString("/Resources")
            });
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
