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
using System.Globalization;
using LHDTV.Repo;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;


using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using LHDTV.Service;
using LHDTV.Helpers;
using AutoMapper;
using Serilog;

using Microsoft.AspNetCore.Identity;

namespace LHDTV
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

            services.AddLocalization(opts => opts.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(opts =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("es-ES"),
                };
                opts.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("es-ES");
                opts.SupportedCultures = supportedCultures;

            });
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build();
                });
            });

            services.AddDbContext<LHDTV.Models.DbEntity.LHDTVContext>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            //modulo photos
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<IPhotoService, PhotoService>();
            services.AddTransient<IPhotoRepo, PhotoRepoDb>();
            services.AddSingleton<Fakes.Fakes>(new Fakes.Fakes());


            //modulo folders
            services.AddTransient<IFolderService, FolderService>();
            services.AddTransient<IFolderRepo, FolderRepoDb>();

            //modulo users
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepoDb, UserRepoDb>();

            services.AddTransient<IMailService, MailService>();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });


            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
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

            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseSerilogRequestLogging();


            app.UseRouting();

            app.UseAuthorization();

            var supportedCultures = new[]
            {
                new CultureInfo("es-Es"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("es-Es"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });
            app.UseCors("EnableCORS");


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

    }
}

