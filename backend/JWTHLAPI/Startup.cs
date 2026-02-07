using JWTHLAPI.BusinessLayer.Manager.Auth;
using JWTHLAPI.BusinessLayer.Manager.Hotel;
//using JWTHLAPI.BusinessLayer.Manager.Lodge;
using JWTHLAPI.DataLayer;
using JWTHLAPI.DataLayer.Interfaces.Auth;
using JWTHLAPI.DataLayer.Interfaces.Hotel;
//using JWTHLAPI.DataLayer.Interfaces.Lodge;
using JWTHLAPI.DataLayer.Repository.Auth;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JWTHLAPI.Helpers;
using System.Threading.Tasks;
using JWTHLAPI.DataLayer.Repository.Hotel;

namespace JWTHLAPI
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
            // ---------- DB CONTEXT ----------
            services.AddSingleton<HMDBContext>();
            //---------- HELPERS ----------
            services.AddScoped<JwtTokenHelper>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<PermissionManager>();

            // ---------- AUTH ----------
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<AuthManager>();
            //-----------------RolePermission-----------------
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<RolePermissionManager>();

            // ---------- HOTEL ----------
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<CategoryManager>();

            services.AddScoped<ICounterRepository, CounterRepository>();
            services.AddScoped<CounterManager>();

            //// ---------- LODGE ----------
            //services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
            //services.AddScoped<RoomTypeManager>();

            //services.AddScoped<IRoomDetailRepository, RoomDetailRepository>();
            //services.AddScoped<RoomDetailManager>();

            // ---------- JWT AUTH ----------
            var jwtSettings = Configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            services.AddAuthorization();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JWTHLAPI", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer {token}'"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            // ---------- CORS ----------
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAll",
            //        builder =>
            //        {
            //            builder.AllowAnyOrigin()
            //                   .AllowAnyHeader()
            //                   .AllowAnyMethod();
            //        });
            //});
            services.AddCors(options =>
            {
                options.AddPolicy("FrontendPolicy", builder =>
                {
                    builder
                        .WithOrigins(
                            "http://localhost:4200",
                            "http://192.168.0.105:4200",
                            "https://192.168.0.105:4200"
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("AdminOnly", policy =>
            //        policy.RequireClaim("roleId", "1")); 
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "JWTHLAPI v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("FrontendPolicy");

            app.UseAuthentication(); 
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
