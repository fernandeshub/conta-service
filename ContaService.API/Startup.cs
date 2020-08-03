using AutoMapper;
using ContaService.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using FluentValidation;
using ContaService.API.ViewModels;
using ContaService.API.Validation;
using ContaService.Domain.Interfaces;
using ContaService.Service.Services;
using ContaService.API.Mapping;

namespace ContaService.API
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
           services.AddDbContext<ContaServiceContext>(option =>
                option.UseSqlite(Configuration.GetConnectionString("DefaultConnection"),
                 x => x.MigrationsAssembly("ContaService.API"))
            );
            
            services.AddMvc()
            .AddFluentValidation()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAutoMapper(typeof(AutoMapperProfile));

            //AutomapperConfig(services);

             services.AddTransient<IValidator<Lancamento>, LancamentoValidator>();
             services.AddScoped<ILancamentoService, LancamentoService>();

             services.AddSwaggerGen();
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
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Conta Service API V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
