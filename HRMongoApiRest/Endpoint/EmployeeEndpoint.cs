using MongoDB.Driver;

namespace HRMongoApiRest.Endpoint
{
    public static class EmployeeEndpoint
    {
        public static void MapEmployeeEndpoint(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/employee").WithTags("Employees");

            group.MapGet("/", GetAll);            
        }

        private static async Task<IResult> GetAll(IMongoClient client)
        {
            var collection = client.GetDatabase("HumanResources").GetCollection<Employee>("Employees");

            var employees = await collection.Find(FilterDefinition<Employee>.Empty).ToListAsync();            

            return TypedResults.Ok(employees);
        }
    }
}
