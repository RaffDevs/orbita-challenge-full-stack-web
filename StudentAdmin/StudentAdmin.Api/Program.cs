using AlunosAdmin.CrossCutting.IoC;
using Microsoft.EntityFrameworkCore;
using StudentAdmin.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDatabaseContext(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddMediator();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    _ = bool.TryParse(Environment.GetEnvironmentVariable("RUN_MIGRATE"), out var runMigration);
    if (runMigration)
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<StudentAdminDbContext>();
        dbContext.Database.Migrate();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();