using Avro.IO;
using Avro.Specific;
using Confluent.Kafka;
using Contracts;
using Contracts.Configuration;
using Contracts.KafkaTopics;

var consumerConfig = new ConsumerConfig
{
    GroupId = "order-consumer-group",
    BootstrapServers = KafkaConfig.BootstrapServers,
    AutoOffsetReset = AutoOffsetReset.Earliest,
    EnableAutoCommit = false
};

using var consumer = new ConsumerBuilder<Ignore, byte[]>(consumerConfig).Build();
consumer.Subscribe(KafkaTopics.OrderTopic);

while (true)
{
    try
    {
        var consumeResult = consumer.Consume(CancellationToken.None);
        var order = DeserializeAvroMessage(consumeResult.Message.Value);

        Console.WriteLine("-------Order-------");
        Console.WriteLine($"Id = {order.Id}\n Created at {order.CreatedAt}\n Dishes:");
        if (order.Dishes != null & order.Dishes.Count > 0)
        {
            foreach(var dish in order.Dishes)
            {
                Console.WriteLine($"{dish.Title} - {dish.Cost}");
            }
        }

        Console.WriteLine($"      Total cost: {order.TotalCost}");
        Console.WriteLine("-------------------");
        consumer.Commit(consumeResult);
    }
    catch (ConsumeException e)
    {
        Console.WriteLine($"Consume error: {e.Error.Reason}");
    }
}
  
Order DeserializeAvroMessage(byte[] avroData)
{
    
    var userSchema = Order._SCHEMA;

    using (var stream = new MemoryStream(avroData))
    {
        var reader = new BinaryDecoder(stream);
        var datumReader = new SpecificDatumReader<Order>(userSchema, userSchema);
        return datumReader.Read(null, reader);
    }
}