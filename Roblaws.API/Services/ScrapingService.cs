using JWTAuthTemplate.Context;
using JWTAuthTemplate.Models.Loblaws;
using Microsoft.EntityFrameworkCore;

namespace JWTAuthTemplate.Services;

public class ScrapingService {
    private readonly ApplicationDbContext _context;
    private readonly string _loblawsBaseUrl;
    private readonly string _loblawsApiKey;
    private readonly string queryString = "?lang=en&date=03052024&pickupType=STORE&storeId=[STOREID]&banner=loblaw";
    
    public ScrapingService(ApplicationDbContext context, IConfiguration config) {
        _context = context;
        _loblawsBaseUrl = config["Loblaws:BaseUrl"] ?? "";
        _loblawsApiKey = config["Loblaws:ApiKey"] ?? "";
        
    }
    
    public async Task<bool> ScrapeNew(string url, string storeId) {
        var strippedUrl = url.Split("?").First();
        var productUrl = GetUrlWithStoreId(url, storeId);
        
        var p = await GetProduct(productUrl);
        
        var dbProduct = new DbProduct {
            Name = p.Name,
            Code = p.Code,
            Brand = p.Brand,
            Description = p.Description,
            Ingredients = p.Ingredients.ToString(),
            PackageSize = p.PackageSize,
            MchCode = p.MchCode,
            ArticleNumber = p.ArticleNumber,
            Uom = p.Uom,
            Link = p.Link,
            IsVariant = p.IsVariant,
            IsPrimaryVariant = p.IsPrimaryVariant,
            StoreId = storeId,
            OriginalUrl = strippedUrl,
            Created = DateTime.UtcNow,
            LastChecked = DateTime.UtcNow
        };
        
        _context.Products.Add(dbProduct);
        
        foreach (var offer in p.Offers) {
            var o = new DbOffer() {
                DbProductProductId = dbProduct.ProductId,
                Product = dbProduct,
                OfferType = offer.OfferType,
                SellerId = offer.SellerId,
                StockStatus = offer.StockStatus,
                Created = DateTime.UtcNow,
                LastChecked = DateTime.UtcNow
            };
            _context.Offers.Add(o);
            //await _context.SaveChangesAsync();

            var primaryPrice = new DbPrimaryPrice() {
                Offer = o,
                OfferId = o.OfferId,
                Value = offer.Price.Value,
                Unit = offer.Price.Unit,
                Quantity = offer.Price.Quantity,
                ReasonCode = offer.Price.ReasonCode,
                Type = offer.Price.Type,
                ExpiryDate = offer.Price.ExpiryDate,
                Created = DateTime.UtcNow,
                LastChecked = DateTime.UtcNow
            };
            _context.Prices.Add(primaryPrice);
            o.PrimaryPrice = primaryPrice;

            if (offer.ComparisonPrices is not null) {
                foreach (var comparisonPrice in offer.ComparisonPrices) {
                    var cp = new DbComparisonPrice() {
                        Offer = o,
                        OfferId = o.OfferId,
                        Value = comparisonPrice.Value,
                        Unit = comparisonPrice.Unit,
                        Quantity = comparisonPrice.Quantity,
                        ReasonCode = comparisonPrice.ReasonCode,
                        Type = comparisonPrice.Type,
                        ExpiryDate = comparisonPrice.ExpiryDate,
                        Created = DateTime.UtcNow,
                        LastChecked = DateTime.UtcNow
                    };
                    _context.ComparisonPrices.Add(cp);
                    o.ComparisonPrices.Add(cp);
                }
            }

            
            if (offer.MemberOnlyPrice is not null) {
                var memberOnlyPrimaryPrice = new DbMemberOnlyPrimaryPrice() {
                    Offer = o,
                    OfferId = o.OfferId,
                    Value = offer.MemberOnlyPrice.Value,
                    Unit = offer.MemberOnlyPrice.Unit,
                    Quantity = offer.MemberOnlyPrice.Quantity,
                    ReasonCode = offer.MemberOnlyPrice.ReasonCode,
                    Type = offer.MemberOnlyPrice.Type,
                    ExpiryDate = offer.MemberOnlyPrice.ExpiryDate,
                    Created = DateTime.UtcNow,
                    LastChecked = DateTime.UtcNow
                };
                _context.MemberOnlyPrimaryPrices.Add(memberOnlyPrimaryPrice);
                o.MemberOnlyPrimaryPrice = memberOnlyPrimaryPrice;
                
            }

            if (offer.MemberOnlyComparisonPrices is not null) {
                foreach (var memberOnlyComparisonPrice in offer.MemberOnlyComparisonPrices) {
                    var cp = new DbMemberOnlyComparisonPrice() {
                        Offer = o,
                        OfferId = o.OfferId,
                        Value = memberOnlyComparisonPrice.Value,
                        Unit = memberOnlyComparisonPrice.Unit,
                        Quantity = memberOnlyComparisonPrice.Quantity,
                        ReasonCode = memberOnlyComparisonPrice.ReasonCode,
                        Type = memberOnlyComparisonPrice.Type,
                        ExpiryDate = memberOnlyComparisonPrice.ExpiryDate,
                        Created = DateTime.UtcNow,
                        LastChecked = DateTime.UtcNow
                    };
                    _context.MemberOnlyComparisonPrices.Add(cp);
                    o.MemberOnlyComparisonPrices.Add(cp);
                }
            }
            
            

        }
        
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task ScrapeExisting(int productId) {
        var product = await _context.Products.Where(p => p.ProductId == productId)
            .Include(x => x.Offers)
            .ThenInclude(x => x.PrimaryPrice)
            .Include(x => x.Offers)
            .ThenInclude(x => x.ComparisonPrices)
            .Include(x => x.Offers)
            .ThenInclude(x => x.MemberOnlyPrimaryPrice)
            .Include(x => x.Offers)
            .ThenInclude(x => x.MemberOnlyComparisonPrices)
            .AsSplitQuery()
            .FirstOrDefaultAsync();
        
        if (product is null) {
            throw new ArgumentException("Product not found");
        }

        var productUrl = GetUrlWithStoreId(product.OriginalUrl, product.StoreId);

        var p = await GetProduct(productUrl);
        
        var primaryDbPrice = product.Offers.Select(x => x.PrimaryPrice).FirstOrDefault();
        var comparisonDbPrices = product.Offers.Select(x => x.ComparisonPrices).FirstOrDefault();
        var memberOnlyPrimaryDbPrice = product.Offers.Select(x => x.MemberOnlyPrimaryPrice).FirstOrDefault();
        var memberOnlyComparisonDbPrices = product.Offers.Select(x => x.MemberOnlyComparisonPrices).FirstOrDefault();
        
        bool primaryPriceChanged = false;
        bool comparisonPricesChanged = false;
        bool memberOnlyPrimaryPriceChanged = false;
        bool memberOnlyComparisonPricesChanged = false;
        
        var wOffer = p.Offers.First();
        if (wOffer is null) {
            throw new ArgumentException("Product Offer not found");
        }

        if (primaryDbPrice is not null) {
            primaryPriceChanged = !primaryDbPrice.Value.Equals(p.Offers.First().Price.Value);
        }
        
        if (comparisonDbPrices is not null && 
            wOffer.ComparisonPrices is not null && 
            wOffer.ComparisonPrices.Any())
        {
            var comparisonPrices = wOffer.ComparisonPrices?.Select(x => x.Value);
            
            if (comparisonPrices is not null)
            {
                comparisonPricesChanged = !comparisonDbPrices.Select(x => x.Value).SequenceEqual(comparisonPrices);
            }
        }
        
        if (memberOnlyPrimaryDbPrice is not null) {
            memberOnlyPrimaryPriceChanged = !memberOnlyPrimaryDbPrice.Value.Equals(wOffer.MemberOnlyPrice?.Value);
        }
        
        if (memberOnlyComparisonDbPrices is not null && 
            wOffer.MemberOnlyComparisonPrices is not null && 
            wOffer.MemberOnlyComparisonPrices.Any())
        {
            var memberOnlyComparisonPrices = wOffer.MemberOnlyComparisonPrices?.Select(x => x.Value);
            
            if (memberOnlyComparisonPrices is not null)
            {
                memberOnlyComparisonPricesChanged = !memberOnlyComparisonDbPrices.Select(x => x.Value).SequenceEqual(memberOnlyComparisonPrices);
            }
        }
        
        if (primaryPriceChanged || comparisonPricesChanged || memberOnlyPrimaryPriceChanged || memberOnlyComparisonPricesChanged) {
            Console.WriteLine("Updating product");
            foreach (var offer in p.Offers) {
                var o = new DbOffer() {
                    DbProductProductId = product.ProductId,
                    Product = product,
                    OfferType = offer.OfferType,
                    SellerId = offer.SellerId,
                    StockStatus = offer.StockStatus,
                    Created = DateTime.UtcNow,
                    LastChecked = DateTime.UtcNow
                };
                _context.Offers.Add(o);
                

                if (primaryPriceChanged) {
                    var primaryPrice = new DbPrimaryPrice() {
                        Offer = o,
                        OfferId = o.OfferId,
                        Value = offer.Price.Value,
                        Unit = offer.Price.Unit,
                        Quantity = offer.Price.Quantity,
                        ReasonCode = offer.Price.ReasonCode,
                        Type = offer.Price.Type,
                        ExpiryDate = offer.Price.ExpiryDate,
                        Created = DateTime.UtcNow,
                        LastChecked = DateTime.UtcNow
                    };
                    _context.Prices.Add(primaryPrice);
                    o.PrimaryPrice = primaryPrice;
                }

                if (comparisonPricesChanged) {
                    if (offer.ComparisonPrices is not null) {
                        foreach (var comparisonPrice in offer.ComparisonPrices) {
                            var cp = new DbComparisonPrice() {
                                Offer = o,
                                OfferId = o.OfferId,
                                Value = comparisonPrice.Value,
                                Unit = comparisonPrice.Unit,
                                Quantity = comparisonPrice.Quantity,
                                ReasonCode = comparisonPrice.ReasonCode,
                                Type = comparisonPrice.Type,
                                ExpiryDate = comparisonPrice.ExpiryDate,
                                Created = DateTime.UtcNow,
                                LastChecked = DateTime.UtcNow
                            };
                            _context.ComparisonPrices.Add(cp);
                            o.ComparisonPrices.Add(cp);
                            
                        }
                    }
                }


                if (memberOnlyPrimaryPriceChanged) {
                    if (offer.MemberOnlyPrice is not null) {
                        var memberOnlyPrimaryPrice = new DbMemberOnlyPrimaryPrice() {
                            Offer = o,
                            OfferId = o.OfferId,
                            Value = offer.MemberOnlyPrice.Value,
                            Unit = offer.MemberOnlyPrice.Unit,
                            Quantity = offer.MemberOnlyPrice.Quantity,
                            ReasonCode = offer.MemberOnlyPrice.ReasonCode,
                            Type = offer.MemberOnlyPrice.Type,
                            ExpiryDate = offer.MemberOnlyPrice.ExpiryDate,
                            Created = DateTime.UtcNow,
                            LastChecked = DateTime.UtcNow
                        };
                        _context.MemberOnlyPrimaryPrices.Add(memberOnlyPrimaryPrice);
                        o.MemberOnlyPrimaryPrice = memberOnlyPrimaryPrice;

                    }
                }

                if (memberOnlyComparisonPricesChanged) {
                    if (offer.MemberOnlyComparisonPrices is not null) {
                        foreach (var memberOnlyComparisonPrice in offer.MemberOnlyComparisonPrices) {
                            var cp = new DbMemberOnlyComparisonPrice() {
                                Offer = o,
                                Value = memberOnlyComparisonPrice.Value,
                                Unit = memberOnlyComparisonPrice.Unit,
                                Quantity = memberOnlyComparisonPrice.Quantity,
                                ReasonCode = memberOnlyComparisonPrice.ReasonCode,
                                Type = memberOnlyComparisonPrice.Type,
                                ExpiryDate = memberOnlyComparisonPrice.ExpiryDate,
                                Created = DateTime.UtcNow,
                                LastChecked = DateTime.UtcNow
                            };
                            _context.MemberOnlyComparisonPrices.Add(cp);
                            o.MemberOnlyComparisonPrices.Add(cp);   
                        }
                    }
                }

            }

        }
        else {
            Console.WriteLine("No changes detected");
            product.LastChecked = DateTime.UtcNow;
        }
        
        await _context.SaveChangesAsync();
    }

    private async Task<Product> GetProduct(string url) {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("x-apikey", _loblawsApiKey);

        var response = await client.GetAsync(url);
        var p = await response.Content.ReadFromJsonAsync<Product>();
        if (p is null) {
            throw new ArgumentException("Product not found");

        }

        return p;
    }

    public async Task UpdateAll() {
        var products = await _context.Products
            .Where(x => x.LastChecked < DateTime.UtcNow.AddDays(-1))
            .ToListAsync();
        
        foreach (var product in products) {
            await ScrapeExisting(product.ProductId);
        }
    }

    private string GetUrlWithStoreId(string url, string storeId) {
        var productStr = url.Split("/p/").Last();
        var productId = productStr.Split("?").First();
        var productUrl = $"{_loblawsBaseUrl}/{productId}{queryString.Replace("[STOREID]", storeId)}";
        
        return productUrl;
    }
}