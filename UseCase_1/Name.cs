using System.Text.Json.Serialization;

namespace UseCase_1;

public sealed class Name
{
    [JsonPropertyName("common")]
    public string Common { get; set; }
}