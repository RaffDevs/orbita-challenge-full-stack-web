using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using StudentAdmin.Infrastructure.Database;

namespace AlunosAdmin.CrossCutting.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration config)
    {
        string? server = Environment.GetEnvironmentVariable("DB_SERVER") ??
                         config.GetSection("DatabaseDefaultParams")["DB_SERVER"];
        string? port = Environment.GetEnvironmentVariable("DB_PORT") ??
                       config.GetSection("DatabaseDefaultParams")["DB_PORT"];
        string? database = Environment.GetEnvironmentVariable("DB_DATABASE") ??
                           config.GetSection("DatabaseDefaultParams")["DB_DATABASE"];
        string? user = Environment.GetEnvironmentVariable("DB_USER") ??
                       config.GetSection("DatabaseDefaultParams")["DB_USER"];
        string? password = Environment.GetEnvironmentVariable("DB_PASSWORD") ??
                           config.GetSection("DatabaseDefaultParams")["DB_PASSWORD"];
        
        string connectionString = $"Host={server};" +
                                  $"Port={port};" +
                                  $"Pooling=true;" +
                                  $"Database={database};" +
                                  $"User Id={user};" +
                                  $"Password={password};";

        services.AddDbContext<StudentAdminDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        return services;
    }
}