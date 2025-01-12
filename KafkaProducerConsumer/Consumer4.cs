using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace KafkaProducerConsumer
{
    public class Consumer4
    {
        private readonly string _groupId;
        private readonly string _bootstrapServers;
        private readonly string _topic;

        public Consumer4(string groupId, string bootstrapServers, string topic)
        {
            _groupId = groupId;
            _bootstrapServers = bootstrapServers;
            _topic = topic;
        }

        public async Task ConsumeMessagesAsync()
        {
            var config = new ConsumerConfig
            {
                GroupId = _groupId,
                BootstrapServers = _bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe(_topic);

                try
                {
                    while (true)
                    {
                        var consumeResult = consumer.Consume();
                        Console.WriteLine($"{_groupId} - Consumed message: {consumeResult.Message.Value}");
                    }
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error consuming message in {_groupId}: {e.Error.Reason}");
                }
            }
        }
    }
}