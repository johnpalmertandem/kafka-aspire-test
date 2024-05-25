var builder = DistributedApplication.CreateBuilder(args);

var kafkaMessaging = builder.AddKafka("messaging");

var postgresPass = builder.AddParameter("postgresql-password", secret: true);

var messagingDb = builder.AddPostgres("postgresdb", password: postgresPass)
    .WithDataVolume("messagingVolume3")
    .AddDatabase("messagingDb");

builder.AddProject<Projects.AspirePoc_KafkaConsumer>("KafkaConsumer")
    .WithReference(messagingDb)
    .WithReference(kafkaMessaging);

builder.AddProject<Projects.AspirePoc_KafkaProducer>("KafkaProducer")
    .WithReference(kafkaMessaging);

builder.Build().Run();