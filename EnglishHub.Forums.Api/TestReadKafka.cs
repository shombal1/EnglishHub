using System.Text.Json;
using Confluent.Kafka;

namespace EnglishHub.Forums.Api;

public class TestReadKafka(IConsumer<byte[],byte[]> consumer,ILogger<TestReadKafka> logger) : BackgroundService
{
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();

        consumer.Subscribe("EnglishHub.DomainEvents");
        while (!stoppingToken.IsCancellationRequested)
        {
            ConsumeResult<byte[], byte[]>? consumerResult = consumer.Consume();
            
            DomainEvent t = JsonSerializer.Deserialize<DomainEvent>(consumerResult.Message.Value)!;
            
            logger.LogInformation(t.CalledEntity);

            consumer.Commit(consumerResult);
        }
        
        consumer.Close();
    }

    class DomainEvent
    {
        public Guid Id { get; set; } 
        public DateTimeOffset Created { get; set; }
        public string CalledEntity { get; set; } = "";
    }
}