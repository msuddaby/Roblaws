using TypeGen.Core.TypeAnnotations;

namespace JWTAuthTemplate.DTO.Loblaws;

[ExportTsClass]
public class ProductDTO {
    public int ProductId { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Brand { get; set; }
    public bool IsPrimaryVariant { get; set; }
    public string? PackageSize { get; set; }
    public string? MchCode { get; set; }
    public List<OfferDTO> Offers { get; set; } = null!;
    public string? ArticleNumber { get; set; }
    public string? Ingredients { get; set; }
    public bool IsVariant { get; set; }
    public string? Uom { get; set; }
    public string? Link { get; set; }
    public string StoreId { get; set; } = null!;
    public string OriginalUrl { get; set; } = null!;
    public DateTime Created { get; set; }
    public DateTime LastChecked { get; set; }
}