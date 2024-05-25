using Confluent.Kafka;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddServiceDefaults();

builder.AddKafkaProducer<string, string>("messaging");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/produceMessage", (IProducer<string, string> producer) =>
    {
        producer.Produce("topic", new Message<string, string>()
        {
            Value = "this is the key",
            Key = "this is the Key"
        });
    })
    .WithName("ProduceMessage")
    .WithOpenApi();

app.MapDefaultEndpoints();

app.Run();
