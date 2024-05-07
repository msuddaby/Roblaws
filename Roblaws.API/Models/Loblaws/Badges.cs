using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace JWTAuthTemplate.Models.Loblaws;

public class Badges {
    [JsonProperty("textBadge")]
    [NotMapped]
    public object TextBadge { get; set; }

    [JsonProperty("loyaltyBadge")]
    [NotMapped]
    public object LoyaltyBadge { get; set; }

    [JsonProperty("dealBadge")]
    [NotMapped]
    public object DealBadge { get; set; }

    [JsonProperty("newItemBadge")]
    [NotMapped]
    public object NewItemBadge { get; set; }
}