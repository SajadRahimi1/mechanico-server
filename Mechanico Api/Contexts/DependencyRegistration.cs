using System.Text;
using Mechanico_Api.Interfaces;
using Mechanico_Api.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Mechanico_Api.Contexts;

public abstract class DependencyRegistration
{
    public static void RegisterDependencies(WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("mssqlConnection");
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        builder.Services.AddAutoMapper(typeof(Program));

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IJwtRepository, JwtRepository>();
        
        builder.Services.AddScoped<ISmsCodeRepository, SmsCodeRepository>();

        builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context => new ValidationFailedResult(context.ModelState);
        });

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            });
    }
}