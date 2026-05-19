using AllJob.API.Services;
using AllJob.API.Services.Interfaces;
using AllJob.Application.Interfaces.Services.Shared;
using AllJob.Application.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using System.Text;

namespace AllJob.API.Extensions;

    public static class ApiServiceRegistration
    {       
        public static IServiceCollection AddApiService(
            this IServiceCollection services,
             IConfiguration configuration)
        {
            services.AddSingleton<IExceptionResponseService, ExceptionResponseService>();

            services.AddScoped<IFileUploadService,
                CloudinaryService>();
            services.AddScoped<IEmailService, SendGridEmailService>(); 



            services.Configure<JwtSettings>(
        configuration.GetSection("JwtSettings"));

            services.Configure<CloudinarySettings>(
        configuration.GetSection("CloudinarySettings"));

            services.Configure<SendGridSettings>(
                configuration.GetSection("SendGridSettings")); 

            services.Configure<AppSettings>(
        configuration.GetSection("AppSettings"));


            services.Configure<AdminSettings>(
        configuration.GetSection("AdminSettings"));

        services.Configure<GoogleSettings>(
            configuration.GetSection("GoogleSettings"));

        services.Configure<TokenHashSettings>(
    configuration.GetSection("TokenHashSettings"));
        services.AddHttpClient();

        services.Configure<GeminiSettings>(
      configuration.GetSection("GeminiSettings"));


        var jwtSettings = configuration
                .GetSection("JwtSettings")
                .Get<JwtSettings>()!;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = jwtSettings.Issuer,
                            ValidAudience = jwtSettings.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                        };
                    });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AllJob API", Version = "v1" });

                c.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme
                {
                    Name  = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter: Bearer {token}"
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
                    Array.Empty<string>()
                    }


                });
            });


        services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("login", opt =>
            {
                opt.PermitLimit = 5;
                opt.Window = TimeSpan.FromMinutes(1);
                opt.QueueLimit = 0;
            });

            options.AddFixedWindowLimiter("register", opt =>
            {
                opt.PermitLimit = 3;
                opt.Window = TimeSpan.FromHours(1);
                opt.QueueLimit = 0;
            });

            options.AddFixedWindowLimiter("forgotpassword", opt =>
            {
                opt.PermitLimit = 3;
                opt.Window = TimeSpan.FromHours(1);
                opt.QueueLimit = 0;
            });
            options.AddFixedWindowLimiter("twofa", opt =>
            {
                opt.PermitLimit = 5;
                opt.Window = TimeSpan.FromMinutes(15);
                opt.QueueLimit = 0;
            });
            options.AddFixedWindowLimiter("cv-generation", opt =>
            {
                opt.PermitLimit = 20;
                opt.Window = TimeSpan.FromHours(1);
                opt.QueueLimit = 0;
            });

            options.RejectionStatusCode = 429;
        });


        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
       policy
           .WithOrigins(
               "http://localhost:4200",
               "https://your-app.azurestaticapps.net",
               "https://spinning-backpedal-porthole.ngrok-free.dev"
           )
           .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials());
        });


        services.AddMemoryCache();
        services.AddSingleton<ICacheService, CacheService>();




        return services;
        }
    }
