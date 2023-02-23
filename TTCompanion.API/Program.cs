using Microsoft.EntityFrameworkCore;
using TTCompanion.API.FantasyFootball;
using TTCompanion.API.FantasyFootball.DBContexts;
using TTCompanion.API.FantasyFootball.Services;
using TTCompanion.API.FantasyFootball.Services.Player;
using TTCompanion.API.FantasyFootball.Services.Race;
using TTCompanion.API.FantasyFootball.Services.Skill;
using TTCompanion.API.FantasyFootball.Services.SpecialRule;

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
                options.ReturnHttpNotAcceptable = true;
            })
                .AddNewtonsoftJson()
                .AddXmlDataContractSerializerFormatters();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<FFDataStore>();
            builder.Services.AddDbContext<FFContext>(DbContextOptions => DbContextOptions.UseSqlite(builder.Configuration["ConnectionStrings:FFDBConnectionString"]));

            builder.Services.AddScoped<IFFRepository, FFRepository>();
            builder.Services.AddScoped<IFFRaceRepository, FFRaceRepository>();
            builder.Services.AddScoped<IFFSpecialRuleRepository, FFSpecialRuleRepository>();
            builder.Services.AddScoped<IFFPlayerRepository, FFPlayerRepository>();
            builder.Services.AddScoped<IFFSkillRepository, FFSkillRepository>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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