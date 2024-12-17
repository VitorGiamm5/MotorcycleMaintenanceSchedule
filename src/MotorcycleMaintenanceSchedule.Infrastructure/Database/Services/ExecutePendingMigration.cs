using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace MotorcycleMaintenanceSchedule.Infrastructure.Database.Services;

public static class ExecutePendingMigration
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

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
                Logger.Debug($"Apply migrations success");

                dbContext.Database.MigrateAsync().Wait();
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, $"Error to apply migrations: {ex.Message}");

            throw;
        }
    }
}
