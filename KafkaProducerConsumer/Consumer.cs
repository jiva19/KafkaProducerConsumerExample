using Confluent.Kafka;


namespace KafkaProducerConsumer;

public class Consumer
{
    
    
    public static void ConsumeMessages()
    {
        var config = new ConsumerConfig
        {
            GroupId = "test-consumer-group",  // Consumer group ID for the first consumer
            BootstrapServers = "localhost:9092", // Kafka broker address
            AutoOffsetReset = AutoOffsetReset.Earliest  // Start reading from the earliest message if no offset exists
        };

        // Run the first consumer
        RunConsumer("Consumer 1", config);
    
        // After first consumer finishes, run the second consumer
        var secondConfig = new ConsumerConfig
        {
            GroupId = "second-consumer-group",  // Consumer group ID for the second consumer
            BootstrapServers = "localhost:9092", // Kafka broker address
            AutoOffsetReset = AutoOffsetReset.Earliest  // Start reading from the earliest message if no offset exists
        };
        RunConsumer("Consumer 2", secondConfig);
    }

    private static void RunConsumer(string consumerName, ConsumerConfig config)
    {
        using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
        {
            consumer.Subscribe("test-topic"); // Subscribe to the topic

            try
            {
                int messageCount = 0;
                while (messageCount < 5) // For example, consume 10 messages and then stop
                {
                    var consumeResult = consumer.Consume();
                    Console.WriteLine($"{consumerName} - Consumed message: {consumeResult.Message.Value} from partition {consumeResult.Partition}, offset {consumeResult.Offset}");
                    messageCount++;
                }
            }
            catch (ConsumeException e)
            {
                Console.WriteLine($"Error consuming message in {consumerName}: {e.Error.Reason}");
            }
        }
    }
    
    
    
    
}