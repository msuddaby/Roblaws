using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using TypeGen.Core.TypeAnnotations;

namespace JWTAuthTemplate.Models.Loblaws;
[PrimaryKey(nameof(OfferId))]
public class DbOffer {
    public int OfferId { get; set; }
    public int DbProductProductId { get; set; }
    public DbProduct Product { get; set; }
    public int DbPrimaryPriceId { get; set; }
    public DbPrimaryPrice PrimaryPrice { get; set; }
    public List<DbComparisonPrice>? ComparisonPrices { get; set; }
    public int? DbMemberOnlyPrimaryPriceId { get; set; }
    public DbMemberOnlyPrimaryPrice? MemberOnlyPrimaryPrice { get; set; }
    public List<DbMemberOnlyComparisonPrice>? MemberOnlyComparisonPrices { get; set; }
    [MaxLength(1000)]
    public string? SellerId { get; set; }
    [MaxLength(1000)]
    public string? StockStatus { get; set; }
    [MaxLength(1000)]
    public string? OfferType { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastChecked { get; set; }
}