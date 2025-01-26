using Cantine.Exceptions;
using Cantine.Interfaces.Managers;
using Cantine.Interfaces.Stores;
using Cantine.Models;

namespace Cantine.Managers
{
    public class TicketManager : ITicketManager
    {
        private readonly ITicketStore _store;
        private readonly ICustomerStore _customerStore;
        private readonly IProductStore _productStore;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="store">The ticket store instance</param>
        /// <param name="customerStore">The customer store instance</param>
        /// <param name="productStore">The product store instance</param>
        public TicketManager(ITicketStore store, ICustomerStore customerStore, IProductStore productStore)
        {
            _store = store;
            _customerStore = customerStore;
            _productStore = productStore;
        }

        /// <inheritdoc />
        public async Task<int> CreateAsync(int customerId)
        {
            var customer = await _customerStore.GetByIdAsync(customerId);
            if(customer != null)
                return await _store.CreateAsync(customerId);
            throw new CustomerNotFoundException();
        }

        /// <inheritdoc />
        public async Task<Ticket> GetAsync(int id)
        {
            return await _store.GetAsync(id);
        }

        /// <inheritdoc />
        public async Task<bool> AddProductAsync(int id, int productId, int quantity)
        {
            var ticket = await GetAsync(id);
            var product = await _productStore.GetByIdAsync(productId);
            if(product != null)
            {
                var result = await _store.AddProductAsync(id, productId, quantity);
                return result & await _store.SetTotalAmount(id, await CalculateAmount(ticket));
            }
            throw new ProductNotFoundException();
        }

        /// <inheritdoc />
        public async Task<bool> RemoveProductAsync(int id, int productId, int quantity)
        {
            var ticket = await GetAsync(id);
            var product = await _productStore.GetByIdAsync(productId);
            if (product != null)
            {
                var result = await _store.RemoveProductAsync(id, productId, quantity);
                return result & await _store.SetTotalAmount(id, await CalculateAmount(ticket));
            }
            throw new ProductNotFoundException();
        }

        /// <inheritdoc />
        public async Task<bool> PayAsync(int id)
        {
            var ticket = await GetAsync(id);
            var customer = await _customerStore.GetByIdAsync(ticket.CustomerId);

            if (customer.PurseAmount - ticket.TotalAmount < 0)
                throw new InsufficientFundsException();

            if (!await _store.PayAsync(id)) return false;

            return await _customerStore.RemoveFundsAsync(ticket.CustomerId, -1 * ticket.TotalAmount);
        }

        private async Task<double> CalculateAmount(Ticket ticket)
        {
            var customer = await _customerStore.GetByIdAsync(ticket.CustomerId);
            if (customer.Type == CustomerTypeEnum.Vip) return 0; //Shortcut for this case to avoid useless total calculation
            
            var totalAmount = 0d;
            var ticketProducts = new List<Product>();
            foreach (var item in ticket.Items)
            {
                ticketProducts.Add(await _productStore.GetByIdAsync(item.Key));
            }
            var productTypesInTicket = ticketProducts.Select(x => x.Type).Distinct().ToList();
            var starters = await _productStore.GetAllOfProductTypeAsync(ProductTypeEnum.Starter);
            var dishes = await _productStore.GetAllOfProductTypeAsync(ProductTypeEnum.Dish);
            var desserts = await _productStore.GetAllOfProductTypeAsync(ProductTypeEnum.Dessert);
            var breads = await _productStore.GetAllOfProductTypeAsync(ProductTypeEnum.Bread);
            var supplements = await _productStore.GetAllOfProductTypeAsync(ProductTypeEnum.Supplement);

            var hasPackage = productTypesInTicket.Contains(ProductTypeEnum.Starter) && productTypesInTicket.Contains(ProductTypeEnum.Dish) && productTypesInTicket.Contains(ProductTypeEnum.Dessert) && productTypesInTicket.Contains(ProductTypeEnum.Bread);
            if (hasPackage)
            {
                //Apply the package price
                totalAmount += 10;
            }
            
            //Add extra product from package types reduced of 1 if the package is identified
            totalAmount += ticket.Items.Where(x => starters.Exists(y => y.Id == x.Key)).Sum(x => (x.Value - (hasPackage ? 1 : 0)) * ticketProducts.First(y => y.Id == x.Key).Price);
            totalAmount += ticket.Items.Where(x => dishes.Exists(y => y.Id == x.Key)).Sum(x => (x.Value - (hasPackage ? 1 : 0)) * ticketProducts.First(y => y.Id == x.Key).Price);
            totalAmount += ticket.Items.Where(x => desserts.Exists(y => y.Id == x.Key)).Sum(x => (x.Value - (hasPackage ? 1 : 0)) * ticketProducts.First(y => y.Id == x.Key).Price);
            totalAmount += ticket.Items.Where(x => breads.Exists(y => y.Id == x.Key)).Sum(x => (x.Value - (hasPackage ? 1 : 0)) * ticketProducts.First(y => y.Id == x.Key).Price);

            //Add supplements
            totalAmount += ticket.Items.Where(x => supplements.Exists(y => y.Id == x.Key)).Sum(x => x.Value * ticketProducts.First(y => y.Id == x.Key).Price);

            switch (customer.Type)
            {
                default:
                case CustomerTypeEnum.Guest:
                    return totalAmount;
                case CustomerTypeEnum.Vip:
                    return 0;
                case CustomerTypeEnum.Contractor:
                    return Math.Max(totalAmount - 6, 0);
                case CustomerTypeEnum.Employee:
                    return Math.Max(totalAmount - 7.5, 0);
                case CustomerTypeEnum.Trainee:
                    return Math.Max(totalAmount - 10, 0);
            }
        }
    }
}
