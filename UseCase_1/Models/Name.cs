using System.Text.Json.Serialization;

namespace UseCase_1.Models;

public sealed class Name
{
    [JsonPropertyName("common")]
    public string Common { get; set; }
}