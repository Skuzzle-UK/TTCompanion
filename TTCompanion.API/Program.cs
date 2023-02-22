using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTCompanion.API.FantasyFootball;
using TTCompanion.API.FantasyFootball.DBContexts;

namespace TTCompanion.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                //options.ReturnHttpNotAcceptable = true;
                options.Filters.Add(new ProducesAttribute("application/json", "application/xml", "text/plain"));
            }).AddXmlDataContractSerializerFormatters();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<FFDataStore>();
            builder.Services.AddDbContext<FFContext>(DbContextOptions => DbContextOptions.UseSqlite(builder.Configuration["ConnectionStrings:FFDBConnectionString"]));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.MapControllers();

            app.Run();
        }
    }
}