using HRMongoApiRest.Endpoint;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var connectionString = "mongodb+srv://webapp:Admin%241234@cluster0.vfi7une.mongodb.net/?appName=Cluster0";
builder.Services.AddSingleton<IMongoClient>(new MongoClient(connectionString));

builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "myapp.com",
        ValidAudience = "myapp.com",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Xk9mP2vL8qR5tY7wZ3nB6cF1hJ4sD0aG9eU2iO5pM8"))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthorization();
app.UseHttpsRedirection();
app.MapAuthorizationEndpoint();
app.MapEmployeeEndpoint();

app.Run();