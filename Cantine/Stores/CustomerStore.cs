using Cantine.Exceptions;
using Cantine.Interfaces.Stores;
using Cantine.Models;

namespace Cantine.Stores
{
    public class CustomerStore : ICustomerStore
    {
        private static Dictionary<int, Customer> _customers = new()
        {
            { 1, new Customer { Id = 1, FirstName = "Peter", LastName = "Aldrick", Type = CustomerTypeEnum.Contractor } },
            { 2, new Customer { Id = 2, FirstName = "Gérard", LastName = "Majax", Type = CustomerTypeEnum.Employee } },
            { 3, new Customer { Id = 3, FirstName = "Gustave", LastName = "Flaubert", Type = CustomerTypeEnum.Guest } },
            { 4, new Customer { Id = 4, FirstName = "Marc", LastName = "Delage", Type = CustomerTypeEnum.Trainee } },
            { 5, new Customer { Id = 5, FirstName = "Jeff", LastName = "Bezos", Type = CustomerTypeEnum.Vip } }
        };

        /// <summary>
        /// Constructor
        /// </summary>
        public CustomerStore() { }

        /// <inheritdoc />
        public async Task<Customer> GetByIdAsync(int id)
        {
            if (_customers.TryGetValue(id, out var customer))
                return customer;
            throw new CustomerNotFoundException();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return _customers.Values.ToList();
        }

        /// <inheritdoc />
        public async Task<bool> AddFundsAsync(int id, double amount)
        {
            var customer = await GetByIdAsync(id);
            customer.PurseAmount += amount;
            _customers[id] = customer;

            return true;
        }

        /// <inheritdoc />
        public async Task<bool> RemoveFundsAsync(int id, double amount)
        {
            var customer = await GetByIdAsync(id);
            customer.PurseAmount += amount;
            _customers[id] = customer;

            return true;
        }
    }
}
