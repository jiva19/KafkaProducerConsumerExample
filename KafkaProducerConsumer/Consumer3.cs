using Confluent.Kafka;
namespace KafkaProducerConsumer;

public class Consumer3
{
    
    public static async Task ConsumeMessages()
    {
        var config1 = new ConsumerConfig
        {
            GroupId = "test-consumer-group",  // Consumer group ID for the first consumer
            BootstrapServers = "localhost:9092", // Kafka broker address
            AutoOffsetReset = AutoOffsetReset.Earliest  // Start reading from the earliest message if no offset exists
        };

        var config2 = new ConsumerConfig
        {
            GroupId = "second-consumer-group",  // Consumer group ID for the second consumer
            BootstrapServers = "localhost:9092", // Kafka broker address
            AutoOffsetReset = AutoOffsetReset.Earliest  // Start reading from the earliest message if no offset exists
        };

        // Run both consumers concurrently
        var consumer1Task = Task.Run(() => RunConsumer("Consumer 1", config1));
        var consumer2Task = Task.Run(() => RunConsumer("Consumer 2", config2));

        // Wait for both consumers to finish
        await Task.WhenAll(consumer1Task, consumer2Task);
    }

    private static void RunConsumer(string consumerName, ConsumerConfig config)
    {
        using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
        {
            consumer.Subscribe("test-topic"); // Subscribe to the topic

            try
            {
                while (true) // Keep consuming indefinitely
                {
                    var consumeResult = consumer.Consume();
                    Console.WriteLine($"{consumerName} - Consumed message: {consumeResult.Message.Value} from partition {consumeResult.Partition}, offset {consumeResult.Offset}");
                }
            }
            catch (ConsumeException e)
            {
                Console.WriteLine($"Error consuming message in {consumerName}: {e.Error.Reason}");
            }
        }
    }

}