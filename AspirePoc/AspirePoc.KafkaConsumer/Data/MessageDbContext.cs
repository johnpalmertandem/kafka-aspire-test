using AspirePoc.KafkaConsumer.Domain;
using Microsoft.EntityFrameworkCore;

namespace AspirePoc.KafkaConsumer.Data;

public class MessageDbContext(DbContextOptions<MessageDbContext> options) : DbContext(options)
{
    public DbSet<Message> Messages => Set<Message>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("messaging");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MessageDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
        
    }
}