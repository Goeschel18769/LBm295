
using LBm295.Models;
using Microsoft.EntityFrameworkCore;

namespace LBm295
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Details zur Datenbank-Verbindung aus der Konfiguration holen
            var connectionString = builder.Configuration.GetConnectionString("PublisherDB");
            // Verbindung zur Datenbank als Service (für Dependency-Injection) hinzufügen
            builder.Services.AddDbContext<PublisherDB>(options => options.UseSqlServer(connectionString));


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
