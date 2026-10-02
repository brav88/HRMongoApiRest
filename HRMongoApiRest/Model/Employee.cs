using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Employee
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("name")]
    public string? Name { get; set; }

    [BsonElement("email")]
    public string? Email { get; set; }

    [BsonElement("phones")]
    public PhoneDetail? Phones { get; set; }

    [BsonElement("address")]
    public AddressDetail? Address { get; set; }

    [BsonElement("status")]
    public bool? Status { get; set; }
}

public class PhoneDetail
{
    [BsonElement("mobile")]
    public string? Mobile { get; set; }

    [BsonElement("fax")]
    public string? Fax { get; set; }

    [BsonElement("home")]
    public string? Home { get; set; }

    [BsonElement("work")]
    public string? Work { get; set; }
}

public class AddressDetail
{
    [BsonElement("home")]
    public string? Home { get; set; }

    [BsonElement("work")]
    public string? Work { get; set; }
}