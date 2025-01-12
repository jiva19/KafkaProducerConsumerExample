using Confluent.Kafka;

namespace KafkaProducerConsumer;

public class Consumer2
{
    public static async Task<ConsumeResult<Ignore, string>?> ConsumeMessages(string bootstrapServers)
    {
        var config = new ConsumerConfig
        {
            GroupId = "test-consumer-group",  // Consumer group ID
            BootstrapServers = bootstrapServers, // Kafka broker address
            AutoOffsetReset = AutoOffsetReset.Earliest  // Start reading from the earliest message if no offset exists
        };

        // Initialize the consumer
        using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
        {
            consumer.Subscribe("test-topic"); // Subscribe to topic

            try
            {
                // Consume a message (you can set a timeout here if you want to wait for a message for a certain period)
                var consumeResult = consumer.Consume(TimeSpan.FromSeconds(10)); // Adjust the timeout as necessary

                // If we successfully consumed a message, return it
                if (consumeResult != null)
                {
                    Console.WriteLine($"Consumed message: {consumeResult.Message.Value} from partition {consumeResult.Partition}, offset {consumeResult.Offset}");
                }

                return consumeResult; // Return the consumed message
            }
            catch (ConsumeException e)
            {
                // Handle any consume exceptions
                Console.WriteLine($"Error consuming message: {e.Error.Reason}");
                return null; // Return null in case of error
            }
        }
    }
}
