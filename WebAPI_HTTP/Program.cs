
using Microsoft.EntityFrameworkCore;
using WebAPI_HTTP.DBContext;

namespace WebAPI_HTTP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddLogging(configure => configure.AddConsole());

            builder.Services.AddDbContext<AddAppContext>(options =>
             options.UseSqlServer("Server=OGN-LT31\\MSSQLSERVER03;Database=master;Trusted_Connection=True;TrustServerCertificate=Yes"));

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
