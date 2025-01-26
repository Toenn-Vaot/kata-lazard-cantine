using Cantine.Interfaces.Managers;
using Cantine.Interfaces.Stores;
using Cantine.Managers;
using Cantine.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cantine.Api
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

            builder.Services.AddSingleton<ICustomerManager, CustomerManager>();
            builder.Services.AddSingleton<ICustomerStore, CustomerStore>();
            builder.Services.AddSingleton<IProductManager, ProductManager>();
            builder.Services.AddSingleton<IProductStore, ProductStore>();
            builder.Services.AddSingleton<ITicketManager, TicketManager>();
            builder.Services.AddSingleton<ITicketStore, TicketStore>();

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
