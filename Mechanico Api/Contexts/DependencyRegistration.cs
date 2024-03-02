using Microsoft.EntityFrameworkCore;

namespace Mechanico_Api.Contexts;

public abstract class DependencyRegistration
{
    public static void RegisterDependencies(WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("mssqlConnection");
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        
        builder.Services.AddAutoMapper(typeof(Program));
    }
}