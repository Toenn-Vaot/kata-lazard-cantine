using Cantine.Interfaces.Managers;
using Cantine.Interfaces.Stores;
using Cantine.Models;

namespace Cantine.Managers
{
    public class ProductManager : IProductManager
    {
        private readonly IProductStore _store;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="store">The product store instance</param>
        public ProductManager(IProductStore store)
        {
            _store = store;
        }

        /// <inheritdoc />
        public async Task<Product> GetById(int id)
        {
            return await _store.GetByIdAsync(id);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _store.GetAllAsync();
        }

        /// <inheritdoc />
        public async Task<Product> GetByName(string name)
        {
            return await _store.GetByNameAsync(name);
        }

        /// <inheritdoc />
        public async Task<List<Product>> GetAllOfProductType(ProductTypeEnum productType)
        {
            return await _store.GetAllOfProductTypeAsync(productType);
        }
    }
}
