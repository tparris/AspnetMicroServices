using Discount.Grpc.Extensions;
using Discount.Grpc.Repositories;
using Discount.Grpc.Services;
//using Discount.Grpc.Services;

namespace Discount.Grpc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
            builder.Services.AddAutoMapper(typeof(Program));
            // Additional configuration is required to successfully run gRPC on macOS.
            // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

            // Add services to the container.
            builder.Services.AddGrpc();

            var app = builder.Build();
            app.MigrateDatabase<Program>();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<DiscountService>();
            //app.UseEndpoints(endpoints =>
            //{
            //    app.MapGrpcService<DiscountService>();
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client.TTo learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
            //    });
            //});


            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}