using Microsoft.EntityFrameworkCore;

namespace TransactionService.Data.PrerpDb;

public static class MigrationsExtension
{
    public static void ApplyMigrations(this WebApplication app)
    {
        var dbContext = app.Services.GetService<AppDbContext>();
        if (dbContext is not null && app.Environment.IsProduction())
        {
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
}