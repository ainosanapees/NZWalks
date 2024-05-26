using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NZWalks.Api.Data;
using NZWalks.Api.Repositories;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "NZWalks API", Version = "v1" });
        });

        builder.Services.AddDbContext<NZWalksDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalks"));
        });

        builder.Services.AddScoped<IRegionRepository, RegionRepository>();
        builder.Services.AddScoped<IWalkRepository, WalkRepository>();
        builder.Services.AddScoped<IWalkDifficultyRepository, WalkDifficultyRepository>();
        builder.Services.AddAutoMapper(typeof(Program).Assembly);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NZWalks API v1"));
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
