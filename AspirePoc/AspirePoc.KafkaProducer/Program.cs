using System.Reflection;
using AspirePoc.ServiceDefaults;
using Confluent.Kafka;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.AddKafkaProducer<string, string>("messaging");

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/produceMessage", async (IProducer<string, string> producer, [FromServices] ILogger<Program> logger, [FromBody] TestEvent eventMessage, [FromServices] IValidator<TestEvent> validator) =>
    {
        var validationResult = await validator.ValidateAsync(eventMessage);

        if (!validationResult.IsValid) {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }
        
        var message = new Message<string, string>()
        {
            Value = "this is the key",
            Key = "this is the Key"
        };
        
        logger.LogInformation("Producing message");

        try
        {
            producer.Produce("topic", message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return Results.Ok();
    })
    .WithName("ProduceMessage")
    .WithOpenApi();



app.MapDefaultEndpoints();

//TODO: add health checks, add some form of db persistence, add error handling, add kafka connect, add open telemetry, add validation

app.Run();
