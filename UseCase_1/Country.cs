using System.Text.Json.Serialization;

namespace UseCase_1;

public sealed class Country
{
    [JsonPropertyName("name")]
    public Name Name { get; set; }

    [JsonPropertyName("population")]
    public int Population { get; set; }
}