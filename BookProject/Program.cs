
using BookProject.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace BookProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            //EntityFramework beállítás
            builder.Services.AddDbContext<BookDbContext>(options =>
            {
                options.UseMySql(builder.Configuration.GetConnectionString("BookDbContext"),
                ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("BookDbContext")));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
