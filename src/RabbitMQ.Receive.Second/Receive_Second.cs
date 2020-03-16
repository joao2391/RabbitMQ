using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQ.Receive.Second
{
    class Receive_Second
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! 2nd Receive");

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

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received2 {0}", message);

                        Cliente cliente = JsonConvert.DeserializeObject<Cliente>(message);
                        Console.WriteLine("Cliente deserializado2 ->" + cliente.ToString());

                    };
                    channel.BasicConsume(queue: "hello",
                                         autoAck: true,
                                         consumer: consumer);


                    Console.WriteLine(" Press2 [enter] to exit.");
                    Console.ReadLine();
                }
            }


        }
    }


    class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public override string ToString()
        {
            return Id.ToString() + "_" + Nome;

        }
    }
}
