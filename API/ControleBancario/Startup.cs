﻿namespace ControleBancario
{
    using System;
    using System.IO;
    using AutoMapper;
    using System.Text;
    using System.Reflection;
    using Microsoft.OpenApi.Models;
    using ControleBancario.Services;
    using System.Collections.Generic;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using ControleBancario.MappingModel;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Authentication.JwtBearer;

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
            services.AddCors();
            services.AddMvcCore().AddNewtonsoftJson();
            var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]);
            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddControllers();
            services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("MyWebAPIConnection"));
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            new RegisterService(ref services);

            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations(true);
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Controle Bancário",
                    Version = "v1",
                    Description = "Uma aplicação feita em ASP.NET CORE WEB API",
                    TermsOfService = new Uri("https://github.com/gustavpereira"),
                    Contact = new OpenApiContact
                    {
                        Name = "Gustavo Antonio Pereira",
                        Email = "gugupereira123@hotmail.com",
                        Url = new Uri("https://www.facebook.com/gustavo.antoniopereira.77/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use sobre a licensa ",
                        Url = new Uri("https://github.com/GUSTAVPEREIRA/ControleBancario/blob/master/LICENSE")
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization, o Header usa o Bearer Scheme. Para utilizar a autorização use ('Bearer' + 'BearerToken')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

                MappingConfig mappingConfig = new MappingConfig();
                IMapper mapper = mappingConfig.GetMapperConfiguration().CreateMapper();
                services.AddSingleton(mapper);

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);                
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

            //Detalhe importante, sempre que for configurar as autenticação, e necesário primeiro authenticar e depous autorizar nesta ordem.
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "ControleBancario API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}