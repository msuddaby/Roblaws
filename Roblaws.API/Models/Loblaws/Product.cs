using Newtonsoft.Json;

namespace JWTAuthTemplate.Models.Loblaws;

public class Product {
    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("brand")]
    public string Brand { get; set; }

    [JsonProperty("imageAssets")]
    public List<ImageAsset> ImageAssets { get; set; }

    [JsonProperty("isPrimaryVariant")]
    public bool IsPrimaryVariant { get; set; }

    [JsonProperty("packageSize")]
    public string PackageSize { get; set; }

    [JsonProperty("mchCode")]
    public string MchCode { get; set; }

    [JsonProperty("offers")]
    public List<Offer> Offers { get; set; }

    [JsonProperty("pricingUnits")]
    public PricingUnits PricingUnits { get; set; }

    [JsonProperty("breadcrumbs")]
    public List<Breadcrumb> Breadcrumbs { get; set; }

    [JsonProperty("articleNumber")]
    public string ArticleNumber { get; set; }

    [JsonProperty("ingredients")]
    public object Ingredients { get; set; }

    [JsonProperty("nutritionFacts")]
    public List<object> NutritionFacts { get; set; }

    [JsonProperty("isVariant")]
    public bool IsVariant { get; set; }

    [JsonProperty("variantTheme")]
    public object VariantTheme { get; set; }

    [JsonProperty("variantGroupId")]
    public object VariantGroupId { get; set; }

    [JsonProperty("uom")]
    public string Uom { get; set; }

    [JsonProperty("variants")]
    public List<Product> Variants { get; set; }

    [JsonProperty("link")]
    public string Link { get; set; }

    [JsonProperty("averageWeight")]
    public object AverageWeight { get; set; }
}