namespace WordFrequency.Infrastructure.Extensions;

using Application.Interfaces;
using Application.UseCases;
using Data;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Services;

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

        services.AddHttpClient<UrlFetcherService>();

        services.AddScoped<IAnalysisRepository, AnalysisRepository>();
        services.AddScoped<IWordFrequencyAnalyzer, WordFrequencyService>();
        services.AddScoped<IHtmlParserService, HtmlParserService>();
        services.AddScoped<IUrlFetcherService>(sp => sp.GetRequiredService<UrlFetcherService>());
        services.AddScoped<IWordFrequencyService, AnalyzeTextUseCase>();

        return services;
    }
}
