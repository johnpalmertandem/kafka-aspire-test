using Confluent.Kafka;

namespace AspirePoc.KafkaConsumer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConsumer<Ignore, string> _consumer;

    public Worker(ILogger<Worker> logger, IConsumer<Ignore, string> consumer)
    {
        _logger = logger;
        _consumer = consumer;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        long i = 0;
        return Task.Factory.StartNew(async () =>
        {
            _consumer.Subscribe("topic");
            while (!stoppingToken.IsCancellationRequested)
            {
                ConsumeResult<Ignore, string>? result = default;
                try
                {
                    result = _consumer.Consume(TimeSpan.FromSeconds(1));
                }
                catch (ConsumeException ex) when (ex.Error.Code == ErrorCode.UnknownTopicOrPart)
                {
                    await Task.Delay(100);
                    continue;
                }
                i++;
                if (i % 1000 == 0)
                {
                    _logger.LogInformation($"Received {i} messages. current offset is '{result!.Offset}'");
                }
            }
        }, stoppingToken, TaskCreationOptions.LongRunning, TaskScheduler.Current);
    }
}