﻿using Domains.Auth.RoleEntity;
using Domains.Auth.UserEntity;
using Infra.EfCore.Auth.Extensions;
using Infra.EFCore.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infra.EFCore.Extensions;



public static class IServiceCollectionExtensions {
    public static void AddInfrastructureServices(this IServiceCollection services , IConfiguration configuration) {
        services.Add_EFCore_Auth_Services();
        services.AddDbContext<AppDbContext>(opt => {
            opt.UseSqlServer(configuration.GetDefaultConnectionString());
        });
        services.AddIdentity<AppUser , AppRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        services.AddAuthentication(opt => {
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt => {
            var jwtSettings = configuration.GetJwtSettings();
            opt.TokenValidationParameters = new TokenValidationParameters {
                ValidateIssuerSigningKey = true ,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)) ,
                ValidateIssuer = true ,
                ValidIssuer = jwtSettings.Issuer ,
                ValidateAudience = true ,
                ValidAudience = jwtSettings.Audience ,
                RequireExpirationTime = true ,
                ClockSkew = TimeSpan.Zero ,
                SaveSigninToken = true ,
            };
        });
    }
}