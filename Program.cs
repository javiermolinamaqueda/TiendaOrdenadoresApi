using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.EntityFrameworkCore;
using NLog;
using System.Configuration;
using TiendaOrdenadoresWebApi.CrossCuting.Logging;
using TiendaOrdenadoresWebApi.Data;
using TiendaOrdenadoresWebApi.QueueService;
using TiendaOrdenadoresWebApi.Services;
using TiendaOrdenadoresWebApi.Services.Conexiones;
using TiendaOrdenadoresWebApi.Validadores.VComponente;
using TiendaOrdenadoresWebApi.Validadores.VOrdenador;

namespace TiendaOrdenadoresWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            LogManager.Setup().LoadConfigurationFromFile("/nlog.config");
            //builder.Services.AddSqlite<TiendaContext>
            //    (builder.Configuration.GetConnectionString("AppConnection"));
            AppDomain.CurrentDomain.SetData("DataDirectory", Directory.GetCurrentDirectory());
            builder.Services.AddDbContext<TiendaContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection")));

            builder.Services.AddScoped<IRepositorioComponente, RepositorioComponente>();
            builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
            builder.Services.AddScoped<IRepositorioOrdenador, RepositorioOrdenador>();
            builder.Services.AddScoped<IValidadorComponente, ValidadorComponente>();
            builder.Services.AddScoped<IRepositorioPedido, RepositorioPedido>();
            builder.Services.AddScoped<IValidadorOrdenador, ValidadorOrdenador>();
            builder.Services.AddScoped<IConexion, Conexion>();

            ////Consumidor de cola de mensaje
            //string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");

            //QueueClient queueClient = new QueueClient(connectionString, "mystoragequeue");

            //builder.Services.AddSingleton(queueClient);
            //builder.Services.AddTransient<IQueueService, AzureQueueService>();

            //
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            //app.CreateDbIfNotExists();

            //Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        
    }
}