using Amazon.KeyManagementService;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Spotifalso.API.Middlewares;
using Spotifalso.Aplication.Inputs;
using Spotifalso.Aplication.Interfaces.Infrastructure;
using Spotifalso.Aplication.Interfaces.Repositories;
using Spotifalso.Aplication.Interfaces.Services;
using Spotifalso.Aplication.Mapping;
using Spotifalso.Aplication.Services;
using Spotifalso.Aplication.Validators;
using Spotifalso.Infrastructure.AWS;
using Spotifalso.Infrastructure.Data.Config;
using Spotifalso.Infrastructure.Data.Repositories;

namespace Spotifalso.API
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Spotifalso.API", Version = "v1" });
            });

            #region Middlewares

            services.AddTransient<ExceptionHandlingMiddleware>();

            #endregion

            #region AutoMapper
            services.AddAutoMapper(typeof(Startup));
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            #endregion

            #region Infrastructure

            //AWS services
            services.AddAWSService<IAmazonKeyManagementService>();

            services.AddDbContextPool<SpotifalsoDBContext>(builder =>
            {
                var mySQLConnection = Configuration.GetConnectionString(nameof(SpotifalsoDBContext));
                builder.UseMySql(mySQLConnection, ServerVersion.AutoDetect(mySQLConnection));
            });

            services.AddScoped<IUserRepository, UserRepository>();

            #endregion

            #region AplicationServices

            services.AddScoped<IValidator<UserInput>, UserValidator>();
            services.AddScoped<IKeyManagementService, KeyManagementService>();
            services.AddScoped<IUserService, UserService>();

            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Spotifalso.API v1"));
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

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
