using ContaService.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ContaService.API.Infrastructure.Filters;
using ContaService.API.Infrastructure.AutofacModules;
using ContaService.API.Controllers;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using ContaService.API.Infrastructure.Auth;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Any;

namespace ContaService.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ILifetimeScope AutofacContainer { get; private set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.AddEntityFrameworkSqlite()
                .AddDbContext<ContaServiceContext>(options =>
                {
                    options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"),
                    sqliteOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                    });
                }, ServiceLifetime.Scoped
            );

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            })
            .AddApplicationPart(typeof(ContaCorrenteController).Assembly)
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors(options =>
           {
               options.AddPolicy("CorsPolicy",
                   builder => builder
                   .SetIsOriginAllowed((host) => true)
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials());
           });

           services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "https://securetoken.google.com/contaservice-e62f3";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "https://securetoken.google.com/contaservice-e62f3", 
                    ValidateAudience = true,
                    ValidAudience = "contaservice-e62f3",
                    ValidateLifetime = true
                };
                options.RequireHttpsMetadata = false;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ContaService API", Version = "v1" });
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri("/api/v1/authorization",UriKind.Relative),
                            Extensions = new Dictionary<string, IOpenApiExtension>
                                {
                                    { "returnSecureToken", new OpenApiBoolean(true) },
                                },
                        }

                    }
                });

                 c.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            var container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new ApplicationModule(Configuration.GetConnectionString("DefaultConnection")));

            AutofacContainer = container.Build();
 
            return new AutofacServiceProvider(AutofacContainer);
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

            app.UseAuthentication();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Conta Service API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
