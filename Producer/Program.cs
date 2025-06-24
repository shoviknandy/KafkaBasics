using System.Text.Json;
using Confluent.Kafka;
using EventContract;


// Configuring Kafka Producer
var config = new ProducerConfig
{
    BootstrapServers = "localhost:9092"
};
 // This builds a kafka producer
 //Null- since we are not using keys, just plain messages
 // string - we will use Json, so keeping it string
 // using - we want the producer to dispose automatically when done
using var producer = new ProducerBuilder<Null, string>(config).Build();

//Creating a new ORder
var order = new OrderCreate
{
    OrderId = Guid.NewGuid(),
    CustomerName = "Shovik Nandy",
    TotalAmount = 1299.50m,
    CreatedAt=DateTime.UtcNow
};


// making the kafka message
var message = new Message<Null, string>
{
    Value = JsonSerializer.Serialize(order)
};

try
{
    // sends the message to Kafka topic "order-events"
    var result = await producer.ProduceAsync("order-events", message);
    Console.WriteLine($"Sent order to kafka {order}");

    // logging where the message was delivered
    Console.WriteLine($"Delivered to {result.TopicPartitionOffset}");

}
catch (ProduceException<Null,string> msg)
{
    Console.WriteLine($"Error Occured {msg.Error.Reason}");
}
