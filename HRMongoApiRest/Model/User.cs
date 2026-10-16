using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    [BsonElement("email")]
    public string? Email { get; set; }

    [BsonElement("alias")]
    public string? Alias { get; set; }

    [BsonElement("pwd")]
    public string? Pwd { get; set; }

    [BsonElement("status")]
    public int Status { get; set; }

    [BsonElement("token")]
    public string? Token { get; set; } 
}