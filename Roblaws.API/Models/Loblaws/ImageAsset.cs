using Newtonsoft.Json;

namespace JWTAuthTemplate.Models.Loblaws;

public class ImageAsset {
    [JsonProperty("imageUrl")]
    public object ImageUrl { get; set; }

    [JsonProperty("smallUrl")]
    public Uri SmallUrl { get; set; }

    [JsonProperty("mediumUrl")]
    public Uri MediumUrl { get; set; }

    [JsonProperty("largeUrl")]
    public Uri LargeUrl { get; set; }

    [JsonProperty("smallRetinaUrl")]
    public Uri SmallRetinaUrl { get; set; }

    [JsonProperty("mediumRetinaUrl")]
    public Uri MediumRetinaUrl { get; set; }

    [JsonProperty("largeRetinaUrl")]
    public Uri LargeRetinaUrl { get; set; }
}