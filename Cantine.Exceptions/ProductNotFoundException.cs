using Cantine.Models;

namespace Cantine.Exceptions;

/// <summary>
/// This class describes the base exception triggers when a product was not found
/// </summary>
public class ProductNotFoundException : ItemNotFoundException<Product>
{
}