using System.Text.Json.Serialization;

namespace wpf.ui.model.payloads
{
    public class Station
    {
        public int Id { get; set; }

        [JsonPropertyName("stationCode")]
        public required string StationCode { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        public required string Type { get; set; }
    }
}
