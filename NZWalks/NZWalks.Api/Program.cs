using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NZWalks.Api.Data;
using NZWalks.Api.Repositories;
using System.Text;

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
        builder.Services.AddSingleton<IUserRepository, StaticUserRepository>();
        builder.Services.AddAutoMapper(typeof(Program).Assembly);
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))

            }) ; 
        ;

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NZWalks API v1"));
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
