using TypeGen.Core.TypeAnnotations;

namespace JWTAuthTemplate.DTO.Loblaws;
[ExportTsClass]
public class OfferDTO {
    public int OfferId { get; set; }
    public int PrimaryPriceId { get; set; }
    public PrimaryPriceDTO PrimaryPrice { get; set; }
    public List<ComparisonPriceDTO>? ComparisonPrices { get; set; }
    public int? MemberPrimaryPriceId { get; set; }
    public MemberOnlyPriceDTO? MemberOnlyPrimaryPrice { get; set; }
    public List<MemberOnlyComparisonPriceDTO>? MemberOnlyComparisonPrices { get; set; }
    public string? SellerId { get; set; }
    public string? StockStatus { get; set; }
    public string? OfferType { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastChecked { get; set; }
}