// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using Json.Schema;
using Json.Schema.Generation;

var schemaBuilder = new JsonSchemaBuilder();

var config = new SchemaGeneratorConfiguration()
{
    Nullability = Nullability.AllowForNullableValueTypes,
    PropertyNameResolver = PropertyNameResolvers.CamelCase
};

var jobSchema = schemaBuilder.FromType<Author>(config);
jobSchema.Title($"E.ON ectocloud {typeof(Author).Name}");
var jobJson = JsonSerializer.Serialize(jobSchema.Build(), new JsonSerializerOptions() { WriteIndented = true });
File.WriteAllText(Guid.NewGuid().ToString(), jobJson, System.Text.Encoding.UTF8);

public static class SecretLevels
{
    public const string VerySecret = "verySecret";
    public const string Secret = "secret";
    public const string NotSecret = "notSecret";
}

[If(nameof(SecretLevel), SecretLevels.NotSecret, ShouldHaveAddress)]
[If(nameof(SecretLevel), SecretLevels.Secret, ShouldHaveAddress)]
public class Author
{
    public const string ShouldHaveAddress = nameof(ShouldHaveAddress);

    [Required]
    public string Name { get; set; }

    [Required]
    public string Age { get; set; }

    [Required]
    public string SecretLevel { get; set; }

    [Required(ConditionGroup = ShouldHaveAddress)]
    [Nullable(true)]
    public AuthorAddress? Address { get; set; }
}

public class AuthorAddress
{
    [Required]
    public string Street { get; set; }

    [Required]
    public string ZipCode { get; set; }
}