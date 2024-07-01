using Confluent.Kafka;

namespace AspirePoc.KafkaConsumer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConsumer<string, string> _consumer;

    public Worker(ILogger<Worker> logger, IConsumer<string, string> consumer)
    {
        _logger = logger;
        _consumer = consumer;
        _consumer.Subscribe("topic");
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() =>
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("just running ");
                try
                {
                    var result = _consumer.Consume(stoppingToken);
                    _logger.LogInformation($"Consumed message '{result.Message.Value}' at: '{result.Topic}'.");
                }
                catch (ConsumeException e)
                {
                    _logger.LogError($"Consume error: {e.Error.Reason}");
                }
            }
        }, stoppingToken);
    }
}