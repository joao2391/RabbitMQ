using System;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;

namespace RabbitMQ.Send
{
    class Send
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Hello World! {nameof(Send)}");

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                    string message = "Hello World!";
                    string message2 = "Hello World2!";
                    

                    Cliente cliente = new Cliente
                    {
                        Id = 1,
                        Nome = "Joao Teste Um"
                    };

                    message = JsonConvert.SerializeObject(cliente);
                    

                    var body = Encoding.UTF8.GetBytes(message);
                    var body2 = Encoding.UTF8.GetBytes(message2);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body2);

                    Console.WriteLine(" [x] Sent {0}", message);
                    Console.WriteLine(" [x] Sent {0}", message2);
                }
            }

           

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();



        }
    }

    class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; }
    }
}
