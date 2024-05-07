using Newtonsoft.Json;

namespace JWTAuthTemplate.Models.Loblaws;

public class Taxes {
    [JsonProperty("gst")]
    public bool Gst { get; set; }

    [JsonProperty("gstExemptTaxFlag")]
    public string GstExemptTaxFlag { get; set; }

    [JsonProperty("hst")]
    public bool Hst { get; set; }

    [JsonProperty("ppft")]
    public bool Ppft { get; set; }

    [JsonProperty("preparedFoodTaxType")]
    public string PreparedFoodTaxType { get; set; }

    [JsonProperty("pst")]
    public bool Pst { get; set; }

    [JsonProperty("spft")]
    public bool Spft { get; set; }
}