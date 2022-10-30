using Microsoft.EntityFrameworkCore;

namespace StorageService.Data.DbExtensions;

public static class MigrationsExtension
{
    public static void ApplyMigrations(this WebApplication app)
    {
        var dbContext = app.Services.GetService<AppDbContext>();

        if (dbContext is null || !app.Environment.IsProduction()) return;

        try
        {
            dbContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            app.Logger.LogError($"Could not apply migrations. {ex.Message}");
            throw;
        }
    }
}
