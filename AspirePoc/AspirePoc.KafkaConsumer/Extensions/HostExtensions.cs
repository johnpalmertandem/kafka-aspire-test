using AspirePoc.KafkaConsumer.Data;
using Microsoft.EntityFrameworkCore;

namespace AspirePoc.KafkaConsumer.Extensions;

public static class HostExtensions
{
    public static void ApplyMigrations(this IHost host)
    {
    
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<Worker>>();
        try
        {
            logger.LogInformation("adding migrations");
            var context = services.GetRequiredService<MessageDbContext>();
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            logger.LogWarning("An error occurred while migrating the database: {exception}", ex.Message);
        }
    }
    
}