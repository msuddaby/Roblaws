using Newtonsoft.Json;

namespace JWTAuthTemplate.Models.Loblaws;

public class Breadcrumb {
    [JsonProperty("categoryCode")]
    public string CategoryCode { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }
}