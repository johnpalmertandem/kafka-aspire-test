using AspirePoc.KafkaConsumer.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspirePoc.KafkaConsumer.Data.EntityConfiguration;

public class MessageConfiguration: IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}