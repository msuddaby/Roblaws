using Newtonsoft.Json;

namespace JWTAuthTemplate.Models.Loblaws;

public class Offer {
    [JsonProperty("price")]
    public Price Price { get; set; }

    [JsonProperty("wasPrice")]
    public Price WasPrice { get; set; }

    [JsonProperty("comparisonPrices")]
    public List<Price>? ComparisonPrices { get; set; }

    [JsonProperty("memberOnlyPrice")]
    public Price? MemberOnlyPrice { get; set; }

    [JsonProperty("memberOnlyComparisonPrices")]
    public List<Price>? MemberOnlyComparisonPrices { get; set; }

    [JsonProperty("sellerId")]
    public string SellerId { get; set; }

    [JsonProperty("sellerName")]
    public object SellerName { get; set; }

    [JsonProperty("promotions")]
    public List<Promotion> Promotions { get; set; }

    [JsonProperty("memberOnlyPromotions")]
    public object MemberOnlyPromotions { get; set; }

    [JsonProperty("stockStatus")]
    public string StockStatus { get; set; }

    [JsonProperty("taxes")]
    public Taxes Taxes { get; set; }

    [JsonProperty("fees")]
    public object Fees { get; set; }

    [JsonProperty("shoppable")]
    public bool Shoppable { get; set; }

    [JsonProperty("specifications")]
    public object Specifications { get; set; }

    [JsonProperty("offerId")]
    public object OfferId { get; set; }

    [JsonProperty("offerType")]
    public string OfferType { get; set; }

    [JsonProperty("status")]
    public object Status { get; set; }

    [JsonProperty("badges")]
    public Badges Badges { get; set; }

    [JsonProperty("dealPrice")]
    public object DealPrice { get; set; }

    [JsonProperty("mopDealPrice")]
    public object MopDealPrice { get; set; }
}