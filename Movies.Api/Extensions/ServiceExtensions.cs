using Microsoft.EntityFrameworkCore;
using Movies.Core;
using Movies.Data;
using Movies.Data.Repositories;
using Movies.Services;

namespace Movies.Api.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(configuration.GetConnectionString("ApplicationContext") ?? throw new InvalidOperationException("Connection string 'ApplicationContext' not found.")));
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IActorRepository, ActorRepository>();
        services.AddScoped(provider => new Lazy<IMovieRepository>(provider.GetRequiredService<IMovieRepository>));
        services.AddScoped(provider => new Lazy<IReviewRepository>(provider.GetRequiredService<IReviewRepository>));
        services.AddScoped(provider => new Lazy<IActorRepository>(provider.GetRequiredService<IActorRepository>));
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITransactionManager, TransactionManager>();
        services.AddScoped<IServiceManager, ServiceManager>();
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IActorService, ActorService>();
        services.AddScoped(provider => new Lazy<IMovieService>(provider.GetRequiredService<IMovieService>));
        services.AddScoped(provider => new Lazy<IReviewService>(provider.GetRequiredService<IReviewService>));
        services.AddScoped(provider => new Lazy<IActorService>(provider.GetRequiredService<IActorService>));

    }
}
