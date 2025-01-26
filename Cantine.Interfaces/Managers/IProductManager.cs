using Cantine.Models;

namespace Cantine.Interfaces.Managers
{
    public interface IProductManager
    {
        /// <summary>
        /// Get the product describes by its identifier
        /// </summary>
        /// <param name="id">The product identifier</param>
        /// <returns>The product if its found. Otherwise, null.</returns>
        Task<Product> GetById(int id);

        /// <summary>
        /// Get all the products
        /// </summary>
        /// <returns>The collection of products.</returns>
        Task<IEnumerable<Product>> GetAllAsync();
        
        /// <summary>
        /// Get the product describes by its name
        /// </summary>
        /// <param name="name">The product name</param>
        /// <returns>The product if its found. Otherwise, null.</returns>
        Task<Product> GetByName(string name);

        /// <summary>
        /// Get all the product of the described type
        /// </summary>
        /// <param name="productType">The product type</param>
        /// <returns>The collection of products</returns>
        Task<List<Product>> GetAllOfProductType(ProductTypeEnum productType);
    }
}
