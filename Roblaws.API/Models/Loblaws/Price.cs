using Newtonsoft.Json;

namespace JWTAuthTemplate.Models.Loblaws;

public class Price {
    [JsonProperty("value")]
    public double? Value { get; set; }

    [JsonProperty("unit")]
    public string? Unit { get; set; }

    [JsonProperty("quantity")]
    public double? Quantity { get; set; }

    [JsonProperty("reasonCode")]
    public int? ReasonCode { get; set; }

    [JsonProperty("type")]
    public string? Type { get; set; }

    [JsonProperty("expiryDate")]
    public string? ExpiryDate { get; set; }
}