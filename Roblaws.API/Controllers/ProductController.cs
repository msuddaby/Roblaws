using JWTAuthTemplate.Context;
using JWTAuthTemplate.DTO;
using JWTAuthTemplate.DTO.Loblaws;
using JWTAuthTemplate.Models.Loblaws;
using JWTAuthTemplate.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWTAuthTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController: ControllerBase {
    
    private readonly ApplicationDbContext _context;
    private readonly ScrapingService _scrapingService;
    
    public ProductController(ApplicationDbContext context, ScrapingService scrapingService) {
        _context = context;
        _scrapingService = scrapingService;
    }
    

    [HttpPost("Scrape")]
    public async Task<ActionResult> Scrape(string url, string storeId) {
        var strippedUrl = url.Split("?").First();
        
        var exists = await _context.Products.AnyAsync(p => p.OriginalUrl == strippedUrl && p.StoreId == storeId);
        
        if (exists) {
            return BadRequest("Product already exists");
        }

        try {
            var result = await _scrapingService.ScrapeNew(url, storeId);
        } catch (ArgumentException e) {
            return BadRequest(e.Message);
        }
        
        
        
        
        return Ok("Product saved to database");
    }
    
    [HttpPost("Update")]
    public async Task<ActionResult> Update(string url, string storeId) {
        
        var prod = await _context.Products.FirstOrDefaultAsync(p => p.OriginalUrl == url && p.StoreId == storeId);
        if (prod == null) {
            return BadRequest("Product does not exist");
        }
        
        await _scrapingService.ScrapeExisting(prod.ProductId);
        
        
        
        return Ok("Product updated");
    }
    
    
    [HttpPost("UpdateAll")]
    public async Task<ActionResult> UpdateAll() {
        await _scrapingService.UpdateAll();
        
        return Ok("All products updated");
    }
    
    [HttpGet("List")]
    public async Task<ActionResult> ListPaged(int page = 1, int pageSize = 10) {
        if (pageSize > 100) {
            pageSize = 100;
        }
        
        var products = await _context.Products
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new ProductDTO {
                Brand = x.Brand,
                Code = x.Code,
                Created = x.Created,
                Description = x.Description,
                Ingredients = x.Ingredients,
                IsPrimaryVariant = x.IsPrimaryVariant,
                IsVariant = x.IsVariant,
                Link = x.Link,
                MchCode = x.MchCode,
                Name = x.Name,
                OriginalUrl = x.OriginalUrl,
                PackageSize = x.PackageSize,
                ProductId = x.ProductId,
                StoreId = x.StoreId,
                Uom = x.Uom,
                LastChecked = x.LastChecked,
                ArticleNumber = x.ArticleNumber
            })
            .ToListAsync();

        var count = await _context.Products.CountAsync();
        
        var result = new PaginatedResult<List<ProductDTO>>() {
            Data = products,
            Page = page,
            PageSize = pageSize,
            TotalCount = count,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize)
        };
        
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id) {
        var result = await _context.Products
            .Where(x => x.ProductId == id)
            .Include(x => x.Offers)
            .ThenInclude(x => x.PrimaryPrice)
            .Include(x => x.Offers)
            .ThenInclude(x => x.ComparisonPrices)
            .Include(x => x.Offers)
            .ThenInclude(x => x.MemberOnlyPrimaryPrice)
            .Include(x => x.Offers)
            .ThenInclude(x => x.MemberOnlyComparisonPrices)
            .Select(x => new ProductDTO {
                Brand = x.Brand,
                Code = x.Code,
                Created = x.Created,
                Description = x.Description,
                Ingredients = x.Ingredients,
                IsPrimaryVariant = x.IsPrimaryVariant,
                IsVariant = x.IsVariant,
                Link = x.Link,
                MchCode = x.MchCode,
                Name = x.Name,
                OriginalUrl = x.OriginalUrl,
                PackageSize = x.PackageSize,
                ProductId = x.ProductId,
                StoreId = x.StoreId,
                Uom = x.Uom,
                LastChecked = x.LastChecked,
                ArticleNumber = x.ArticleNumber,
                Offers = x.Offers.Select(o => new OfferDTO {
                    Created = o.Created,
                    LastChecked = o.LastChecked,
                    MemberOnlyPrimaryPrice = o.MemberOnlyPrimaryPrice == null ? null : new MemberOnlyPriceDTO {
                        Created = o.MemberOnlyPrimaryPrice.Created,
                        ExpiryDate = o.MemberOnlyPrimaryPrice.ExpiryDate,
                        IsMemberOnly = o.MemberOnlyPrimaryPrice.IsMemberOnly,
                        IsPrimary = o.MemberOnlyPrimaryPrice.IsPrimary,
                        IsComparisonPrice = o.MemberOnlyPrimaryPrice.IsComparisonPrice,
                        IsMemberOnlyComparisonPrice = o.MemberOnlyPrimaryPrice.IsMemberOnlyComparisonPrice,
                        OfferId = o.MemberOnlyPrimaryPrice.MemberOnlyPrimaryPriceId,
                        Quantity = o.MemberOnlyPrimaryPrice.Quantity,
                        ReasonCode = o.MemberOnlyPrimaryPrice.ReasonCode,
                        Type = o.MemberOnlyPrimaryPrice.Type,
                        Unit = o.MemberOnlyPrimaryPrice.Unit,
                        Value = o.MemberOnlyPrimaryPrice.Value
                    },
                    MemberOnlyComparisonPrices = o.MemberOnlyComparisonPrices == null ? null : o.MemberOnlyComparisonPrices.Select(m => new MemberOnlyComparisonPriceDTO {
                        Created = m.Created,
                        ExpiryDate = m.ExpiryDate,
                        IsMemberOnly = m.IsMemberOnly,
                        IsPrimary = m.IsPrimary,
                        IsComparisonPrice = m.IsComparisonPrice,
                        IsMemberOnlyComparisonPrice = m.IsMemberOnlyComparisonPrice,
                        OfferId = m.OfferId,
                        Quantity = m.Quantity,
                        ReasonCode = m.ReasonCode,
                        Type = m.Type,
                        Unit = m.Unit,
                        Value = m.Value
                    }).ToList(),
                    OfferId = o.OfferId,
                    PrimaryPrice = new PrimaryPriceDTO {
                        Created = o.PrimaryPrice.Created,
                        ExpiryDate = o.PrimaryPrice.ExpiryDate,
                        IsMemberOnly = o.PrimaryPrice.IsMemberOnly,
                        IsPrimary = o.PrimaryPrice.IsPrimary,
                        IsComparisonPrice = o.PrimaryPrice.IsComparisonPrice,
                        IsMemberOnlyComparisonPrice = o.PrimaryPrice.IsMemberOnlyComparisonPrice,
                        OfferId = o.PrimaryPrice.PrimaryPriceId,
                        PrimaryPriceId = o.PrimaryPrice.PrimaryPriceId,
                        Quantity = o.PrimaryPrice.Quantity,
                        ReasonCode = o.PrimaryPrice.ReasonCode,
                        Type = o.PrimaryPrice.Type,
                        Unit = o.PrimaryPrice.Unit,
                        Value = o.PrimaryPrice.Value
                    },
                    ComparisonPrices = o.ComparisonPrices == null ? null : o.ComparisonPrices.Select(c => new ComparisonPriceDTO() {
                        Created = c.Created,
                        ExpiryDate = c.ExpiryDate,
                        IsMemberOnly = c.IsMemberOnly,
                        IsPrimary = c.IsPrimary,
                        IsComparisonPrice = c.IsComparisonPrice,
                        IsMemberOnlyComparisonPrice = c.IsMemberOnlyComparisonPrice,
                        OfferId = c.ComparisonPriceId,
                        Quantity = c.Quantity,
                        ReasonCode = c.ReasonCode,
                        Type = c.Type,
                        Unit = c.Unit,
                        Value = c.Value
                    }).ToList()
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (result is null) {
            return NotFound();
        }
        
        return Ok(result);
    }


}