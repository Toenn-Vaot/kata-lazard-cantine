using System.Text.Json.Serialization;

namespace Cantine.Models;

/// <summary>
/// This class describes Product
/// </summary>
public class Product
{
    /// <summary>
    /// The identifier
    /// </summary>
    public int Id { get; set; }
        
    /// <summary>
    /// The product name
    /// </summary>
    public string Name { get; set; }
        
    /// <summary>
    /// The product description
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The unit price
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// The product type
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ProductTypeEnum Type { get; set; }
}