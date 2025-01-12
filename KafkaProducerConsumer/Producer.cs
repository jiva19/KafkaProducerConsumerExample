using Confluent.Kafka;


namespace KafkaProducerConsumer;



public class Producer
{
   //public static void Main(string[] args)
   public static void ProduceMessages()
    {
        var config = new ProducerConfig { BootstrapServers = "localhost:9092" }; // Connect to Kafka broker

        using (var producer = new ProducerBuilder<Null, string>(config).Build())
        {
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    var message = $"Message {i}";
                    var dr = producer.ProduceAsync("test-topic", new Message<Null, string> { Value = message }).Result;
                    Console.WriteLine($"Sent message: {message} to partition {dr.Partition}, offset {dr.Offset}");
                    Thread.Sleep(1000);  // Simulate delay
                }
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"Error producing message: {e.Message}");
            }
        }
    }
}