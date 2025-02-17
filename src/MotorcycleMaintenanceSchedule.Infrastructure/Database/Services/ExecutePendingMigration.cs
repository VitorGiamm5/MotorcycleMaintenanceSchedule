using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace MotorcycleMaintenanceSchedule.Infrastructure.Database.Services;

public static class ExecutePendingMigration
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public static void Execute(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            var migrations = dbContext.Database.GetPendingMigrations();

            if (migrations.Any())
            {
                dbContext.Database.MigrateAsync().Wait();

                _logger.Debug($"Apply migrations success");
            }
            else
            {
                _logger.Debug($"No migrations to run");
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error to apply migrations: {ex.Message}");

            throw;
        }
    }
}
