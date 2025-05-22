using NeighborTakeHome.Data;
using NeighborTakeHome.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Injecting singleton representation of JSON data as Data is not changing from run to run and a DB is not needed
//if needed this could be made a DB call or an API call etc., to pull in the most up to date data.
builder.Services.AddSingleton<ParkingLotsData>();

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
