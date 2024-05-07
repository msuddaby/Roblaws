using TypeGen.Core.TypeAnnotations;

namespace JWTAuthTemplate.DTO.Loblaws;
[ExportTsClass]
public class PrimaryPriceDTO {
    public int PrimaryPriceId { get; set; }
    public int OfferId { get; set; }
    public double? Value { get; set; }
    public string? Unit { get; set; }
    public double? Quantity { get; set; }
    public int? ReasonCode { get; set; }
    public string? Type { get; set; }
    public string? ExpiryDate { get; set; }
    public bool IsMemberOnly { get; set; }
    public bool IsPrimary { get; set; }
    public bool IsComparisonPrice { get; set; }
    public bool IsMemberOnlyComparisonPrice { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastChecked { get; set; }
}