
using Microsoft.EntityFrameworkCore;
using WebApiApp02.Models;

namespace WebApiApp02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // DB���� �ʱ�ȭ 
            builder.Services.AddDbContext<AppDbContext>(
                options => options.UseMySql(
                    builder.Configuration.GetConnectionString("SmartHomeConnection"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("SmartHomeConnection"))
                )
            );

            builder.Services.AddControllers();
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
