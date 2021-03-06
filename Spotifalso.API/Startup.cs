using Amazon.KeyManagementService;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
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
using Spotifalso.Infrastructure.JWT;
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Spotifalso.Infrastructure.Cache;
using Spotifalso.Aplication.Services.Caching;
using Spotifalso.Aplication.Interfaces.Services.Caching;
using Amazon.SimpleNotificationService;
using Nest;
using Spotifalso.Infrastructure.Data.Search;
using System.Text.Json.Serialization;

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

            services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                x.JsonSerializerOptions.WriteIndented = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Spotifalso.API", Version = "v1" });
            });

            #region JWT
            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("JwtSecret").Value);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            #endregion

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
            services.AddAWSService<IAmazonSimpleNotificationService>();

            //MySql DB
            services.AddDbContextPool<SpotifalsoDBContext>(builder =>
            {
                var mySQLConnection = Configuration.GetConnectionString(nameof(SpotifalsoDBContext));
                builder.UseMySql(mySQLConnection, ServerVersion.AutoDetect(mySQLConnection));
            });

            //Redis
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("Redis");
            });
            services.Add(ServiceDescriptor.Singleton<IDistributedCache, RedisCache>());

            //Elastic
            services.AddSingleton<IElasticClient>(SearchConfig.GetClient());

            //Infra services
            services.AddSingleton<ICacheProvider, CacheProvider>();
            services.AddSingleton<IAuthCacheService, AuthCacheService>();
            services.AddScoped<IKeyManagementService, KeyManagementService>();
            services.AddScoped<IArtistNotificationService, ArtistNotificationService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddSingleton<IMusicSearchService, MusicSearchService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<IMusicRepository, MusicRepository>();

            #endregion

            #region Validators

            services.AddScoped<IValidator<UserInput>, UserValidator>();
            services.AddScoped<IValidator<ArtistInput>, ArtistValidator>();
            services.AddScoped<IValidator<MusicInput>, MusicValidator>();

            #endregion

            #region AplicationServices

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<IMusicService, MusicService>();

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
