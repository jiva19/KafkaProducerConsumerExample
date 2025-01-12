using Confluent.Kafka;

namespace KafkaProducerConsumer;

public class Producer2
{
    public static  DeliveryResult<Null,string>? ProduceMessage(string bootstrapservers,string message)
    {
        var config = new ProducerConfig { BootstrapServers = bootstrapservers }; // Connect to Kafka broker

        DeliveryResult<Null, string>? dr;
        using (var producer = new ProducerBuilder<Null, string>(config).Build())
        {
            dr = producer.ProduceAsync("test-topic", new Message<Null, string> { Value = message }).Result;
            Console.WriteLine($"Sent message: {message} to partition {dr.Partition}, offset {dr.Offset}");
        }

        return dr;

    }
    
    
    /*
    // Setup the producer configuration to use the Kafka container's broker
    var producerConfig = new ProducerConfig
        {
            BootstrapServers = _kafkaContainer.GetBootstrapAddress()
        };

        // Create the producer
        using (var producer = new ProducerBuilder<Null, string>(producerConfig).Build())
    {
        // Send a message
        var deliveryResult = await producer.ProduceAsync(_topicName, new Message<Null, string> { Value = _messageValue });
        Assert.IsNotNull(deliveryResult);
        Assert.AreEqual(_messageValue, deliveryResult.Value);
    }*/

}