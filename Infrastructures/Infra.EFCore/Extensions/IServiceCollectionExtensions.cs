﻿using Domains.Auth.RoleEntity;
using Domains.Auth.UserEntity;
using Domains.Messaging.GroupEntity.Repo;
using Domains.Messaging.GroupMemberEntity.Repos;
using Domains.Messaging.GroupMessageEntity.Repos;
using Domains.Messaging.GroupRequestEntity.Repos;
using Infra.EfCore.Auth.Extensions;
using Infra.EFCore.Contexts;
using Infra.EFCore.Repositories.Messaging.Group;
using Infra.EFCore.Repositories.Messaging.Group.Members;
using Infra.EFCore.Repositories.Messaging.Group.Messages;
using Infra.EFCore.Repositories.Messaging.Group.Requests;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Extensions;
using System.Text;

namespace Infra.EFCore.Extensions;



public static class IServiceCollectionExtensions {
    public static void AddInfrastructureServices(this IServiceCollection services , IConfiguration configuration) {
        services.Add_Infra_Auth_Services();
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
            var authTokenSettings = configuration.GetAuthTokenSettings();
            Console.WriteLine(authTokenSettings.ToJson());
            opt.TokenValidationParameters = new TokenValidationParameters {
                ValidateIssuerSigningKey = true ,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authTokenSettings.SecretKey)) ,
                ValidateIssuer = true ,
                ValidIssuer = authTokenSettings.Issuer ,
                ValidateAudience = true ,
                ValidAudience = authTokenSettings.Audience ,
                RequireExpirationTime = true ,
                ClockSkew = TimeSpan.Zero ,
                SaveSigninToken = true ,
                TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authTokenSettings.SecretKey))
            };
           
        });

        // Messaging Services
        services.AddScoped<IGroupCommands, GroupCommands>();
        services.AddScoped<IGroupQueries , GroupQueries>();
        services.AddScoped<IGroupRepo , GroupRepo>();     
        
        services.AddScoped<IGroupAdminCommands , GroupAdminCommands>();
        services.AddScoped<IGroupAdminQueries , GroupAdminQueries>();
        services.AddScoped<IGroupAdminRepo , GroupAdminRepo>();

        services.AddScoped<IGroupRequestRepo,GroupRequestRepo>();
        services.AddScoped<IGroupRequestCommands , GroupRequestCommands>();
        services.AddScoped<IGroupRequestQueries , GroupRequestQueries>();

        services.AddScoped<IGroupMessageCommands , GroupMessageCommands>();
        services.AddScoped<IGroupMessageQueries , GroupMessageQueries>();
        services.AddScoped<IGroupMessageRepo , GroupMessageRepo>();

    }
}
