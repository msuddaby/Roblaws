using Newtonsoft.Json;

namespace JWTAuthTemplate.Models.Loblaws;

public class PricingUnits {
    [JsonProperty("type")]
    public string? Type { get; set; }

    [JsonProperty("unit")]
    public string? Unit { get; set; }

    [JsonProperty("interval")]
    public long Interval { get; set; }

    [JsonProperty("minOrderQuantity")]
    public long MinOrderQuantity { get; set; }

    [JsonProperty("maxOrderQuantity")]
    public long MaxOrderQuantity { get; set; }

    [JsonProperty("weighted")]
    public bool Weighted { get; set; }
}