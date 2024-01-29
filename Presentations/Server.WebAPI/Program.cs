using Apps.Auth.Accounts.Repos;
using Apps.Messaging;

using Infra.EFCore.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalRCore();
builder.Services.AddMediatR(config => {
    config.RegisterServicesFromAssemblies(
        typeof(IAccountRepo).Assembly,
        typeof(App_Messaging_Assembly).Assembly
    );
});
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1" , new OpenApiInfo { Title = "JWEToken" , Version = "v1" });

    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
        {
        Scheme = "bearer",
        BearerFormat = "JWE",
        Name = "JWE Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_** your JWE Bearer token on textbox below!",
        Reference = new OpenApiReference
         {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id , jwtSecurityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        { jwtSecurityScheme, Array.Empty<string>() }
     });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
 
}
app.UseCors(opt=> opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
