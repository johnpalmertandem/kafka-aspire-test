using AspirePoc.KafkaConsumer;
using AspirePoc.KafkaConsumer.Data;
using AspirePoc.KafkaConsumer.Extensions;
using AspirePoc.ServiceDefaults;
using Confluent.Kafka;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHostedService<Worker>();

builder.AddNpgsqlDbContext<MessageDbContext>("messagingDb");

builder.AddKafkaConsumer<string, string>("messaging");

var host = builder.Build();

host.ApplyMigrations();

host.Run();