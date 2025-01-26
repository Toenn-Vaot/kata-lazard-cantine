using Cantine.Exceptions;
using Cantine.Interfaces.Stores;
using Cantine.Models;

namespace Cantine.Stores
{
    public class ProductStore : IProductStore
    {
        private static Dictionary<int, Product> _products = new()
        {
            { 1, new Product { Id = 1, Name = "Boisson", Description = "Une boisson", Price = 1, Type = ProductTypeEnum.Supplement } },
            { 2, new Product { Id = 2, Name = "Fromage", Description = "Un fromage", Price = 1, Type = ProductTypeEnum.Supplement } },
            { 3, new Product { Id = 3, Name = "Pain", Description = "Un morceau de pain", Price = 0.4, Type = ProductTypeEnum.Bread } },
            { 4, new Product { Id = 4, Name = "Petite salade bar", Description = "Une petite salade", Price = 4, Type = ProductTypeEnum.Supplement } },
            { 5, new Product { Id = 5, Name = "Grande salade bar", Description = "Une grande salade", Price = 6, Type = ProductTypeEnum.Supplement } },
            { 6, new Product { Id = 6, Name = "Entrée", Description = "Une entrée", Price = 3, Type = ProductTypeEnum.Starter } },
            { 7, new Product { Id = 7, Name = "Plat", Description = "Un plat", Price = 6, Type = ProductTypeEnum.Dish } },
            { 8, new Product { Id = 8, Name = "Dessert", Description = "Un dessert", Price = 3, Type = ProductTypeEnum.Dessert } }
        };

        /// <inheritdoc />
        public async Task<Product> GetByIdAsync(int id)
        {
            if (_products.TryGetValue(id, out var product))
                return product;
            throw new ProductNotFoundException();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return _products.Values.ToList();
        }

        /// <inheritdoc />
        public async Task<Product> GetByNameAsync(string name)
        {
            var product = _products.FirstOrDefault(x => x.Value.Name == name).Value;
            if (product != null)
                return product;
            throw new ProductNotFoundException();
        }

        /// <inheritdoc />
        public async Task<List<Product>> GetAllOfProductTypeAsync(ProductTypeEnum productType)
        {
            return _products.Where(x => x.Value.Type == productType).Select(x => x.Value).ToList();
        }
    }
}
