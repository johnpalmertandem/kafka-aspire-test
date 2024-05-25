using AspirePoc.KafkaConsumer;
using AspirePoc.KafkaConsumer.Data;
using AspirePoc.KafkaConsumer.Extensions;
using Confluent.Kafka;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();

builder.AddNpgsqlDbContext<MessageDbContext>("messagingDb");

builder.AddKafkaConsumer<Ignore, string>("messaging");

builder.AddServiceDefaults();

var host = builder.Build();

host.ApplyMigrations();

host.Run();