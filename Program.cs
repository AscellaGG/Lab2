using System.Diagnostics;
using Lab2.Models;
using Lab2.Models.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opt =>
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LibraryContext>(opt =>
{
    opt.UseInMemoryDatabase("Library");

    opt.LogTo(m => Debug.WriteLine(m)).EnableSensitiveDataLogging(true);
});

//builder.Services.AddDbContext<LibraryContext>(opt =>
//{
//    var connectionString = builder.Configuration.GetConnectionString("LocalDb");
//    opt.UseSqlServer(connectionString);
//});

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
