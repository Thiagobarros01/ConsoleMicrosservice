using Confluent.Kafka;
using System.Text.Json;

var config = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "pedidos-consumer-group",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using var consumer = new ConsumerBuilder<Null, string>(config).Build();
consumer.Subscribe("pedidos");

Console.WriteLine("Aguardando pedidos...");

while (true)
{
    var mensagem = consumer.Consume();
    var pedido = JsonSerializer.Deserialize<Pedido>(mensagem.Message.Value);

    Console.WriteLine($"Pedido recebido: Id={pedido?.Id}, Cliente={pedido?.Cliente}, Data={pedido?.Data}");
}

record Pedido
{
    public Guid Id { get; init; }
    public string Cliente { get; init; }
    public DateTime Data { get; init; }
}
