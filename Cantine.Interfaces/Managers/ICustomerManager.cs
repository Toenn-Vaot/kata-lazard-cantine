using Cantine.Models;

namespace Cantine.Interfaces.Managers
{
    public interface ICustomerManager
    {
        /// <summary>
        /// Get the customer describes by its identifier
        /// </summary>
        /// <param name="id">The customer identifier</param>
        /// <returns>The customer if its found. Otherwise, null.</returns>
        Task<Customer> GetByIdAsync(int id);

        /// <summary>
        /// Get all the customers
        /// </summary>
        /// <returns>The collection of customers</returns>
        Task<IEnumerable<Customer>> GetAllAsync();

        /// <summary>
        /// Add the amount to the customer's purse
        /// </summary>
        /// <param name="id">The customer identifier</param>
        /// <param name="amount">The amount to add</param>
        /// <returns>True if the operation succeed. Otherwise, false.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Triggers when the amount is negative</exception>
        Task<bool> AddFundsAsync(int id, double amount);
        
        /// <summary>
        /// Remove the amount to the customer's purse
        /// </summary>
        /// <param name="id">The customer identifier</param>
        /// <param name="amount">The amount to remove</param>
        /// <returns>True if the operation succeed. Otherwise, false.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Triggers when the amount is positive</exception>
        Task<bool> RemoveFundsAsync(int id, double amount);
    }
}
