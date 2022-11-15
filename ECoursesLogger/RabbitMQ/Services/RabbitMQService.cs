using ECoursesLogger.Data.Entities;
using ECoursesLogger.Data.Interfaces;
using ECoursesLogger.RabbitMQ.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace ECoursesLogger.RabbitMQ.Services
{
    public class RabbitMQService : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        private readonly ICommandMessageRepository _commandMessageRepository;
        private readonly RabbitMQOptions _rabbitMQOptions;
        private readonly ILogger<RabbitMQService> _logger;

        public RabbitMQService(IOptions<RabbitMQOptions> options, ICommandMessageRepository commandMessageRepository,
            ILogger<RabbitMQService> logger)
        {
            _rabbitMQOptions = options.Value;
            _commandMessageRepository = commandMessageRepository;
            _logger = logger;

            var factory = new ConnectionFactory() { HostName = _rabbitMQOptions.HostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _rabbitMQOptions.QueueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += OnRecieved;

            _channel.BasicConsume(queue: _rabbitMQOptions.QueueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        public void OnRecieved(object? obj, BasicDeliverEventArgs eventArgs)
        {
            var content = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

            _logger.Log(LogLevel.Information, $"Message recieved: {content}");

            var message = JsonSerializer.Deserialize<CommandMessage>(content);

            _commandMessageRepository.Create(message!);
        }           
        
        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
