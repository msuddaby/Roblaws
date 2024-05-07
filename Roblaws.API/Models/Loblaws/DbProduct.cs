using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using TypeGen.Core.TypeAnnotations;

namespace JWTAuthTemplate.Models.Loblaws;

[PrimaryKey(nameof(ProductId))]
public class DbProduct {
    public int ProductId { get; set; }
    [MaxLength(100)]
    public string Code { get; set; } = null!;
    [MaxLength(1000)]
    public string Name { get; set; } = null!;
    [MaxLength(5000)]
    public string? Description { get; set; }
    [MaxLength(1000)]
    public string? Brand { get; set; }
    public bool IsPrimaryVariant { get; set; }
    [MaxLength(1000)]
    public string? PackageSize { get; set; }
    [MaxLength(1000)]
    public string? MchCode { get; set; }
    public List<DbOffer> Offers { get; set; } = null!;
    [MaxLength(1000)]
    public string? ArticleNumber { get; set; }
    [MaxLength(5000)]
    public string? Ingredients { get; set; }
    public bool IsVariant { get; set; }
    [MaxLength(1000)]
    public string? Uom { get; set; }
    [MaxLength(2000)]
    public string? Link { get; set; }
    [MaxLength(100)]
    public string StoreId { get; set; } = null!;
    [MaxLength(1000)]
    public string OriginalUrl { get; set; } = null!;
    public DateTime Created { get; set; }
    public DateTime LastChecked { get; set; }
}