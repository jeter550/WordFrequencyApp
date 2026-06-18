using Microsoft.EntityFrameworkCore;
using WordFrequency.Infrastructure.Data;
using WordFrequency.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Word Frequency API", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

await ApplyMigrationsAsync(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Word Frequency API v1"));
}

app.UseRouting();
app.UseCors("AllowAll");

app.MapControllers();

app.Run();

static async Task ApplyMigrationsAsync(WebApplication app)
{
    using var scope = app.Services.CreateAsyncScope();
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (!await context.Database.CanConnectAsync())
        {
            await context.Database.EnsureCreatedAsync();
            app.Logger.LogInformation("Database created successfully");
        }
        else
        {
            app.Logger.LogInformation("Database already exists and is accessible");
        }
    }
    catch (Microsoft.Data.SqlClient.SqlException ex) when (ex.Number == 1801)
    {
        app.Logger.LogInformation("Database already exists, continuing");
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred while ensuring database");
        throw;
    }
}
