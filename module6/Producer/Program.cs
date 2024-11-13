using Avro.IO;
using Avro.Specific;
using Confluent.Kafka;
using Contracts;
using Contracts.Configuration;
using Contracts.KafkaTopics;

Dish dish1 = new()
{
    Id = Guid.NewGuid(),
    Title = "Something number 1",
    Cost = 33.15,
};
Dish dish2 = new()
{
    Id = Guid.NewGuid(),
    Title = "Not bad",
    Cost = 12.22,
};
Dish dish3 = new()
{
    Id = Guid.NewGuid(),
    Title = "Amazing",
    Cost = 100.55,
};

byte[] avroData;
Order order;
var producerConfig = new ProducerConfig()
{
    BootstrapServers = KafkaConfig.BootstrapServers,
};

while (true)
{
    await Task.Delay(5000);
    order = CreateNewOrder();
    using (var ms = new MemoryStream())
    {
        var writer = new BinaryEncoder(ms);
        var datumWriter = new SpecificDatumWriter<Order>(order.Schema);
        datumWriter.Write(order, writer);
        avroData = ms.ToArray();
    }

    try
    {
        using (var producer = new ProducerBuilder<Null, byte[]>(producerConfig).Build())
        {
            var message = new Message<Null, byte[]>
            {
                Value = avroData
            };

            var deliveryResult = producer.ProduceAsync(KafkaTopics.OrderTopic, message).GetAwaiter().GetResult();
            Console.WriteLine($"Message delivered to {deliveryResult.TopicPartitionOffset}");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
}


Dish GetRandomDish()
{
    Random rnd = new Random();
    var key = rnd.Next(3) + 1;
    
    switch (key)
    {
        case 1:
            return dish1;
        case 2:
            return dish2;
        case 3:
            return dish3;
    }
    return new Dish();
}

Order CreateNewOrder()
{
    Random rnd = new Random();
    var key = rnd.Next(5) + 1;
    var order = new Order();
    order.Id = Guid.NewGuid();
    order.CreatedAt = DateTime.Now.ToString();
    order.Dishes = new List<Dish>();
    for(int i = 0; i < key; i++)
    {
        order.Dishes.Add(GetRandomDish());
        order.TotalCost = (decimal)order.TotalCost + (decimal)order.Dishes[i].Cost;
    }

    return order;
}