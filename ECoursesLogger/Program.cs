using ECoursesLogger.Data;
using ECoursesLogger.Data.Interfaces;
using ECoursesLogger.Data.Repositories;
using ECoursesLogger.RabbitMQ.Configuration;
using ECoursesLogger.RabbitMQ.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ECoursesLogger;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((hostingContext, configuration) =>
            {
                configuration.Sources.Clear();

                configuration.AddJsonFile("appsettings.json");
            })
            .ConfigureServices((hostingContext, services) =>
            {
                var configuration = hostingContext.Configuration;

                services.AddSqlServer<ECoursesLoggerContext>(configuration.GetConnectionString("DockerConnection")!);
                services.Configure<RabbitMQOptions>(configuration.GetSection("RabbitMQ"));

                services.AddTransient<ICommandMessageRepository, CommandMessageRepository>();
                services.AddHostedService<RabbitMQService>();
            })
            .Build();

        using var scope = builder.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetService<ECoursesLoggerContext>()!;

        await dbContext.Database.MigrateAsync();

        await builder.RunAsync();
    }
}
