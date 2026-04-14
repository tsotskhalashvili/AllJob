using AllJob.API.Services;
using AllJob.API.Services.Interfaces;
using AllJob.Application.Interfaces.Services.Shared;
using AllJob.Application.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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




            return services;
        }
    }
