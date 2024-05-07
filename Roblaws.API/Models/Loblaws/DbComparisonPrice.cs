using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using TypeGen.Core.TypeAnnotations;

namespace JWTAuthTemplate.Models.Loblaws;
[PrimaryKey(nameof(ComparisonPriceId))]
public class DbComparisonPrice {
    public int ComparisonPriceId { get; set; }
    public int OfferId { get; set; }
    public DbOffer Offer { get; set; }
    public double? Value { get; set; }
    [MaxLength(1000)]
    public string? Unit { get; set; }
    public double? Quantity { get; set; }
    public int? ReasonCode { get; set; }
    [MaxLength(1000)]
    public string? Type { get; set; }
    [MaxLength(1000)]
    public string? ExpiryDate { get; set; }
    public bool IsMemberOnly { get; set; }
    public bool IsPrimary { get; set; }
    public bool IsComparisonPrice { get; set; }
    public bool IsMemberOnlyComparisonPrice { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastChecked { get; set; }
}