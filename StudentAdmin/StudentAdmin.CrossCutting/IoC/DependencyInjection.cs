using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using StudentAdmin.Application.Commands.CreateStudent;
using StudentAdmin.Application.Validations;
using StudentAdmin.Core.Repositories;
using StudentAdmin.Infrastructure.Database;
using StudentAdmin.Infrastructure.Repositories;

namespace AlunosAdmin.CrossCutting.IoC;

public static class DependencyInjection
{

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IStudentRepository, StudentRepository>();

        return services;
    }
    
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

        services.AddDbContext<StudentAdminDbContext>(options => { options.UseNpgsql(connectionString); });

        return services;
    }

    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(
            config => config.RegisterServicesFromAssembly(typeof(CreateStudentCommand).Assembly)
        );
        return services;
    }

    public static IServiceCollection AddFluentValidator(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<CreateStudentValidator>();

        return services;
    }
}