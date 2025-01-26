using Cantine.Exceptions;
using Cantine.Interfaces.Stores;
using Cantine.Models;

namespace Cantine.Stores
{
    public class TicketStore : ITicketStore
    {
        private static object _lockO = new();
        private static int _nextElementId = 1;
        private static Dictionary<int, Ticket> _tickets = new();

        /// <inheritdoc />
        public async Task<int> CreateAsync(int customerId)
        {
            lock (_lockO)
            {
                var id = _nextElementId;
                _nextElementId++;
                _tickets.Add(id, new Ticket { Id = id, CustomerId = customerId, Items = [], Status = TicketStatusEnum.New, TotalAmount = 0 });
                return id;
            }
        }

        /// <inheritdoc />
        public async Task<Ticket> GetAsync(int id)
        {
            if (_tickets.TryGetValue(id, out var item))
                return item;
            throw new TicketNotFoundException();
        }

        /// <inheritdoc />
        public async Task<bool> AddProductAsync(int id, int productId, int quantity)
        {
            var ticket = await GetAsync(id);
            if (ticket.Status == TicketStatusEnum.Paid) throw new TicketAlreadyPaidException();

            if(!ticket.Items.TryAdd(productId, quantity))
                ticket.Items[productId] += quantity;
            ticket.Status = TicketStatusEnum.WaitingPayment;

            _tickets[id] = ticket;
            return true;
        }

        /// <inheritdoc />
        public async Task<bool> RemoveProductAsync(int id, int productId, int quantity)
        {
            var ticket = await GetAsync(id);
            if (ticket.Status == TicketStatusEnum.Paid) throw new TicketAlreadyPaidException();

            if (!ticket.Items.ContainsKey(productId))
                return false;

            ticket.Items[productId] -= quantity;
            if (ticket.Items[productId] <= 0)
                ticket.Items.Remove(productId);
            if (ticket.Items.Count == 0)
                ticket.Status = TicketStatusEnum.New;

            return true;
        }

        /// <inheritdoc />
        public async Task<bool> SetTotalAmount(int id, double amount)
        {
            var ticket = await GetAsync(id);
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            ticket.TotalAmount = amount;
            _tickets[id] = ticket;
            return true;
        }

        /// <inheritdoc />
        public async Task<bool> PayAsync(int id)
        {
            var item = await GetAsync(id);
            if (item.Status == TicketStatusEnum.Paid) throw new TicketAlreadyPaidException();

            item.Status = TicketStatusEnum.Paid;
            return true;
        }
    }
}
