using Cantine.Interfaces.Managers;
using Cantine.Interfaces.Stores;
using Cantine.Models;

namespace Cantine.Managers
{
    public class CustomerManager : ICustomerManager
    {
        private readonly ICustomerStore _store;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="store">The customer store instance</param>
        public CustomerManager(ICustomerStore store)
        {
            _store = store;
        }

        /// <inheritdoc />
        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _store.GetByIdAsync(id);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _store.GetAllAsync();
        }

        /// <inheritdoc />
        public async Task<bool> AddFundsAsync(int id, double amount)
        {
            if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
            return await _store.AddFundsAsync(id, amount);
        }

        /// <inheritdoc />
        public async Task<bool> RemoveFundsAsync(int id, double amount)
        {
            if (amount > 0) throw new ArgumentOutOfRangeException(nameof(amount));
            return await _store.RemoveFundsAsync(id, amount);
        }
    }
}
