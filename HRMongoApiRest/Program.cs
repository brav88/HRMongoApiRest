using HRMongoApiRest.Endpoint;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var connectionString = "mongodb+srv://xxx:xxx@cluster0.vfi7une.mongodb.net/?appName=Cluster0";
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("xxx"))
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