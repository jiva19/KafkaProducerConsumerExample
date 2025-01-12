
using KafkaProducerConsumer;

namespace KafkaProducerConsumer_;
using Confluent.Kafka;
using Testcontainers.Kafka;
using NUnit.Framework;
using System.Threading.Tasks;
using System;

[TestFixture]
public class KafkaProducerConsumerIntegrationTests
{
    private KafkaContainer _kafkaContainer;
    private readonly string _topicName = "test-topic";
    private readonly string _messageValue = "Hello Kafka";
   
    
    

    [OneTimeSetUp] // Runs once before all tests
    public Task InitializeAsync()
    {
        _kafkaContainer = new KafkaBuilder()
            .WithImage("confluentinc/cp-kafka:6.2.10")
            .Build();
        return  _kafkaContainer.StartAsync();
        
       
    }


    [OneTimeTearDown] // Runs once after all tests
    public async Task DisposeAsync()
    {
        if (_kafkaContainer != null)
        {
            await _kafkaContainer.StopAsync();  // Ensure this is awaited properly
            await _kafkaContainer.DisposeAsync();  // Proper disposal
        }
    }
    
    
    
    
    
    
    [Test]
    public async Task Should_Produce_And_Consume_Message()
    {
        /*// Setup the producer configuration to use the Kafka container's broker
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

        var result=Producer2.ProduceMessage(_kafkaContainer.GetBootstrapAddress(), _messageValue);
        Assert.IsNotNull(result);
        Assert.AreEqual(_messageValue, result.Value);


        var consumerreuslt = Consumer2.ConsumeMessages(_kafkaContainer.GetBootstrapAddress());
        Assert.IsNotNull(consumerreuslt);
        Assert.AreEqual(_messageValue,consumerreuslt.Result.Message.Value);






        /*// Setup the consumer configuration to use the Kafka container's broker
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = _kafkaContainer.GetBootstrapAddress(),
            GroupId = "test-consumer-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };*/

        /*
        // Create the consumer
        using (var consumer = new ConsumerBuilder<Null, string>(consumerConfig).Build())
        {
            // Subscribe to the topic
            consumer.Subscribe(_topicName);

            // Consume the message
            var consumeResult = await Task.Run(() => consumer.Consume(TimeSpan.FromSeconds(10)));
            Assert.IsNotNull(consumeResult);
            Assert.AreEqual(_messageValue, consumeResult.Message.Value);
        }*/
    }
}





