using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TTCompanion.API.FantasyFootball;
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
                options.Filters.Add(new ProducesAttribute(
                    "application/json",
                    "application/xml",
                    "text/json",
                    "text/xml"
                    ));
            })
                .AddNewtonsoftJson()
                .AddXmlDataContractSerializerFormatters();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "TTCompanion.API",
                    Description = "TTCompanion.API Swagger UI"
                });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            builder.Services.AddSingleton<DataStore>();
            builder.Services.AddDbContext<FantasyFootball.DBContexts.DBContext>(DbContextOptions => DbContextOptions.UseSqlite(builder.Configuration["ConnectionStrings:FFDBConnectionString"]));

            builder.Services.AddScoped<IRepository, Repository>();
            builder.Services.AddScoped<IRaceRepository, RaceRepository>();
            builder.Services.AddScoped<ISpecialRuleRepository, SpecialRuleRepository>();
            builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
            builder.Services.AddScoped<ISkillRepository, SkillRepository>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer(
                options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Authentication:Issuer"],
                        ValidAudience = builder.Configuration["Authentication:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
                    };
                });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider
                    .GetRequiredService<FantasyFootball.DBContexts.DBContext>();
                dbContext.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

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