using HRMongoApiRest.Endpoint;
using MongoDB.Driver;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var connectionString = "mongodb+srv://<user>:<pwd@cluster0.vfi7une.mongodb.net/?appName=Cluster0";
builder.Services.AddSingleton<IMongoClient>(new MongoClient(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapEmployeeEndpoint();

app.Run();