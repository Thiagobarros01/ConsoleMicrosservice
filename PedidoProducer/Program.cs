using Confluent.Kafka;
using System.Text.Json;

var config = new ProducerConfig
{
    BootstrapServers = "localhost:9092"
};

using var producer = new ProducerBuilder<Null, string>(config).Build();

while (true)
{
    Console.WriteLine("Digite o nome do cliente (ou 'sair' para finalizar):");
    var cliente = Console.ReadLine();

    if (cliente?.ToLower() == "sair") break;

    var pedido = new
    {
        Id = Guid.NewGuid(),
        Cliente = cliente,
        Data = DateTime.UtcNow
    };

    var mensagem = JsonSerializer.Serialize(pedido);

    await producer.ProduceAsync("pedidos", new Message<Null, string> { Value = mensagem });

    Console.WriteLine($"Pedido enviado: {mensagem}");
}
