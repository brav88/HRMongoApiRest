
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Security.Claims;
using System.Text;

namespace HRMongoApiRest.Endpoint
{
    public static class AuthorizationEndpoint
    {
        public static void MapAuthorizationEndpoint(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api").WithTags("Authorization");

            group.MapGet("/login", Authorize);
        }

        private static async Task<IResult> Authorize(IMongoClient client, String username, String password)
        {
            var collection = client.GetDatabase("HumanResources").GetCollection<User>("Authorization");

            User user = await collection.Find(u => u.Email == username && u.Pwd == password).FirstOrDefaultAsync();

            if (user != null)
            {
                var token = new JsonWebTokenHandler().CreateToken(new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, username)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    Issuer = "myapp.com",
                    Audience = "myapp.com",
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("xxx")),
                                           SecurityAlgorithms.HmacSha256)
                });

                user.Token = token;

                return Results.Ok(new { user });
            }

            return Results.Unauthorized();
        }      
    }
}
