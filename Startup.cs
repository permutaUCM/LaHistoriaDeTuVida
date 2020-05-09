using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using LHDTV.Repo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using LHDTV.Service;
using LHDTV.Helpers;
using AutoMapper;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using System.Linq;

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

            services.AddCors(options =>
            {
                options.AddPolicy(name: "EnableCORS", builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyHeader();
                });
            });
            services.AddControllers();

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
            services.AddSingleton<ITokenRecoveryService, TokenRecoveryService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter into field the word 'Bearer' following by space and JWT",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
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

            });


            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.UTF8.GetBytes(appSettings.Secret);
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
                    ValidateAudience = false,
                    LifetimeValidator = CustomLifeTimeValidator,


                };
            });

            services.AddScoped<IUserService, UserService>();
        }
        private bool CustomLifeTimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken token, TokenValidationParameters @params)
        {
            if (expires != null)
            {
                var _expires = expires > DateTime.UtcNow;
                return _expires;
            }
            return false;
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ITokenRecoveryService tokenService, IPhotoService photoService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("EnableCORS");

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseSerilogRequestLogging();

            app.Use(async (context, next) =>
            {

                if (context.Request.Path.StartsWithSegments("/folder/photos"))
                {

                    var token = tokenService.RecoveryToken(context);
                    var pathSegments = context.Request.Path.Value.Split("_");
                    var photoIdString = pathSegments[pathSegments.Length - 1].Split(".")[0];

                    if (token == null || photoIdString == null)
                    {
                        context.Response.StatusCode = 401;
                        return;
                    }

                    var userId = tokenService.RecoveryId(token);
                    var photoId = Convert.ToInt32(photoIdString);
                    try
                    {
                        var photo = photoService.GetPhoto(photoId, userId);
                        if (photo == null)
                        {
                            context.Response.StatusCode = 401;
                            return;
                        }

                        await next();

                    }
                    catch (Exception)
                    {
                        context.Response.StatusCode = 401;
                        return;
                    }

                }
                await next();
            });
            app.UseRouting();
            app.UseAuthentication();
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


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

    }
}

