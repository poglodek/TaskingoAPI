using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NLog;
using TaskingoAPI.Dto;
using TaskingoAPI.Dto.Entity;
using TaskingoAPI.Dto.User;
using TaskingoAPI.Services.Authentication;
using TaskingoAPI.Services.IRepositories;
using TaskingoAPI.Services.Repositories;

namespace TaskingoAPI
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
            var authSettings = new AuthenticationSettings();

            Configuration.GetSection("Authentication").Bind(authSettings);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(config =>
            {
                config.SaveToken = true;
                config.RequireHttpsMetadata = false;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authSettings.JwtIssuer,
                    ValidAudience = authSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.JwtKey)
                    )
                };
            });
            services.AddControllers().AddFluentValidation();
            services.AddScoped<IValidator<UserCreatedDto>, UserCreatedDtoValidation>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddAutoMapper(typeof(TaskingoMapper).Assembly);
            services.AddSingleton(authSettings);
            services.AddTransient<ErrorHandlingMiddleware>();
            services.AddDbContext<TaskingoDbContext>(options => options.UseSqlServer("Server=.;Database=TaskingoAPI;Trusted_Connection=True;"));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskingoAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskingoAPI v1"));
            }
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();
            var database = serviceProvider.GetService<TaskingoDbContext>();
            database.Database.Migrate();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
