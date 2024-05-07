using Newtonsoft.Json;

namespace JWTAuthTemplate.Models.Loblaws;

public class Promotion {
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("quantity")]
    public object Quantity { get; set; }

    [JsonProperty("savings")]
    public object Savings { get; set; }

    [JsonProperty("value")]
    public object Value { get; set; }

    [JsonProperty("promoCode")]
    public string PromoCode { get; set; }

    [JsonProperty("expiryDate")]
    public DateTimeOffset ExpiryDate { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("promoType")]
    public string PromoType { get; set; }

    [JsonProperty("pricingMethod")]
    public object PricingMethod { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("reward")]
    public object Reward { get; set; }
}