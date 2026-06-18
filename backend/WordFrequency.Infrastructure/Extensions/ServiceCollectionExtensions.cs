namespace WordFrequency.Infrastructure.Extensions;

using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Services;

using Application.Interfaces;
using Application.UseCases;
using Domain.Interfaces;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("WordFrequency.Infrastructure")));

        services.AddScoped<IAnalysisRepository, AnalysisRepository>();
        services.AddScoped<IWordFrequencyAnalyzer, WordFrequencyService>();
        services.AddScoped<IWordFrequencyService, AnalyzeTextUseCase>();

        return services;
    }
}
