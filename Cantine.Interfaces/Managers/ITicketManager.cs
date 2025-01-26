using Cantine.Exceptions;
using Cantine.Models;

namespace Cantine.Interfaces.Managers
{
    public interface ITicketManager
    {
        /// <summary>
        /// Create a new ticket for the customer described by its identifier
        /// </summary>
        /// <param name="customerId">The customer identifier</param>
        /// <returns>The ticket identifier newly created</returns>
        Task<int> CreateAsync(int customerId);

        /// <summary>
        /// Get the ticket described by its identifier
        /// </summary>
        /// <param name="id">The ticket identifier</param>
        /// <returns>The ticket if its found. Otherwise, null.</returns>
        Task<Ticket> GetAsync(int id);

        /// <summary>
        /// Add the product with the associated quantity to the ticket described by its identifier
        /// </summary>
        /// <param name="id">The ticket identifier</param>
        /// <param name="productId">The product identifier</param>
        /// <param name="quantity">The quantity</param>
        /// <returns>True if the operation succeed. Otherwise, false.</returns>
        Task<bool> AddProductAsync(int id, int productId, int quantity);
        
        /// <summary>
        /// Remove the product with the associated quantity to the ticket described by its identifier
        /// </summary>
        /// <param name="id">The ticket identifier</param>
        /// <param name="productId">The product identifier</param>
        /// <param name="quantity">The quantity</param>
        /// <returns>True if the operation succeed. Otherwise, false.</returns>
        Task<bool> RemoveProductAsync(int id, int productId, int quantity);

        /// <summary>
        /// Pay the ticket described by its identifier
        /// </summary>
        /// <param name="id">The ticket identifier</param>
        /// <returns>True if the operation succeed. Otherwise, false.</returns>
        /// <exception cref="InsufficientFundsException">Triggers when the customer has not enough funds to pay the ticket</exception>
        Task<bool> PayAsync(int id);
    }
}
