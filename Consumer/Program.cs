using System.Text.Json;
using Confluent.Kafka;
using EventContract;

//Configure the consumer
var config = new ConsumerConfig
{
    BootstrapServers="localhost:9092",
    GroupId="order-consumer-group",
    AutoOffsetReset=AutoOffsetReset.Earliest,
    EnableAutoCommit=true
};

//
using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
consumer.Subscribe("order-events");

Console.WriteLine("Listening for order events");

try
{
    while (true)
    {
        var result = consumer.Consume();
        var order = JsonSerializer.Deserialize<OrderCreate>(result.Message.Value);
        Console.WriteLine("✅ Order received:");
        Console.WriteLine($"🆔 ID: {order?.OrderId}");
        Console.WriteLine($"👤 Customer: {order?.CustomerName}");
        Console.WriteLine($"💵 Amount: ₹{order?.TotalAmount}");
        Console.WriteLine($"⏰ At: {order?.CreatedAt}");
        Console.WriteLine(new string('-', 40));
    }
}
catch (OperationCanceledException)
{
    Console.WriteLine("Consumer Stopeed.");
}
finally
{
    consumer.Close();
}


