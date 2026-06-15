using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Infrastructure.Persistence;
using Lab10DiegoQuispe.Infrastructure.Persistence.Repositories;
using Lab10DiegoQuispe.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Lab10DiegoQuispe.Infrastructure.Services;
using Lab10DiegoQuispe.Application.Interfaces;

namespace Lab10DiegoQuispe.Infrastructure.Configuration;

public static class InfrastructureServicesExtensions
{
	public static IServiceCollection AddInfrastructureServices(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("DefaultConnection");
		if (string.IsNullOrWhiteSpace(connectionString))
		{
			throw new InvalidOperationException("DefaultConnection is not configured.");
		}
		

		services.AddDbContext<AppDbContext>(options =>
			options.UseNpgsql(connectionString));
		services.AddScoped<IJwtTokenService, JwtTokenService>();
		
		services.AddScoped<IBackgroundJobService, BackgroundJobService>();
		services.AddScoped<INotificationService, NotificationService>();

		services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		return services;
	}
}