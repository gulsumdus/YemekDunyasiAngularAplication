using Microsoft.EntityFrameworkCore;
using YemekD�nyasi.Models;


var builder = WebApplication.CreateBuilder(args);
///****
builder.Services.AddDbContext<YemekD�nyasContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("YemekApp")));
///***

// Add services to the container.

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

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
