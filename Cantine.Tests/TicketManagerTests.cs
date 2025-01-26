using Cantine.Exceptions;
using Cantine.Managers;
using Cantine.Models;
using Cantine.Stores;

namespace Cantine.Tests
{
    public class TicketManagerTests
    {
        [Fact]
        public async Task CreateTicket()
        {
            var customerStore = new CustomerStore();
            var productStore = new ProductStore();
            var ticketStore = new TicketStore();
            var ticketManager = new TicketManager(ticketStore, customerStore, productStore);

            var id = await ticketManager.CreateAsync(1);
            Assert.Equal(1, id);
        }

        [Fact]
        public async Task CreateTicketWithUnknownCustomerThrowException()
        {
            var customerStore = new CustomerStore();
            var productStore = new ProductStore();
            var ticketStore = new TicketStore();
            var ticketManager = new TicketManager(ticketStore, customerStore, productStore);

            await Assert.ThrowsAsync<CustomerNotFoundException>(async () => { await ticketManager.CreateAsync(-1); });
        }

        [Fact]
        public async Task AddOneProductToTicket()
        {
            var customerStore = new CustomerStore();
            var productStore = new ProductStore();
            var ticketStore = new TicketStore();
            var ticketManager = new TicketManager(ticketStore, customerStore, productStore);
            
            var id = await ticketManager.CreateAsync(1);
            await ticketManager.AddProductAsync(1, 1, 1);

            var ticket = await ticketManager.GetAsync(1);
            Assert.True(ticket.Items.Count == 1);
            Assert.True(ticket.Items.ContainsKey(1));
            Assert.Equal(1, ticket.Items[1]);
        }

        [Fact]
        public async Task RemoveOneProductOnFiveToTicket()
        {
            var customerStore = new CustomerStore();
            var productStore = new ProductStore();
            var ticketStore = new TicketStore();
            var ticketManager = new TicketManager(ticketStore, customerStore, productStore);
            
            var id = await ticketManager.CreateAsync(1);
            await ticketManager.AddProductAsync(1, 1, 5);
            await ticketManager.RemoveProductAsync(1, 1, 1);

            var ticket = await ticketManager.GetAsync(1);
            Assert.True(ticket.Items.Count == 1);
            Assert.True(ticket.Items.ContainsKey(1));
            Assert.Equal(4, ticket.Items[1]);
        }

        [Fact]
        public async Task RemoveFiveProductOnFiveToTicket()
        {
            var customerStore = new CustomerStore();
            var productStore = new ProductStore();
            var ticketStore = new TicketStore();
            var ticketManager = new TicketManager(ticketStore, customerStore, productStore);
            
            var id = await ticketManager.CreateAsync(1);
            await ticketManager.AddProductAsync(1, 1, 5);
            await ticketManager.RemoveProductAsync(1, 1, 5);

            var ticket = await ticketManager.GetAsync(1);
            Assert.True(ticket.Items.Count == 0);
        }

        [Fact]
        public async Task RemoveSixProductOnFiveToTicket()
        {
            var customerStore = new CustomerStore();
            var productStore = new ProductStore();
            var ticketStore = new TicketStore();
            var ticketManager = new TicketManager(ticketStore, customerStore, productStore);
            
            var id = await ticketManager.CreateAsync(1);
            await ticketManager.AddProductAsync(1, 1, 5);
            await ticketManager.RemoveProductAsync(1, 1, 6);

            var ticket = await ticketManager.GetAsync(1);
            Assert.True(ticket.Items.Count == 0);
        }

        [Fact]
        public async Task AddProductToTicketWithUnknownProductThrowException()
        {
            var customerStore = new CustomerStore();
            var productStore = new ProductStore();
            var ticketStore = new TicketStore();
            var ticketManager = new TicketManager(ticketStore, customerStore, productStore);
            
            var id = await ticketManager.CreateAsync(1);
            await Assert.ThrowsAsync<ProductNotFoundException>(async () => { await ticketManager.AddProductAsync(1, -1, 1); });
        }

        [Fact]
        public async Task RemoveProductToTicketWithUnknownProductThrowException()
        {
            var customerStore = new CustomerStore();
            var productStore = new ProductStore();
            var ticketStore = new TicketStore();
            var ticketManager = new TicketManager(ticketStore, customerStore, productStore);
            
            var id = await ticketManager.CreateAsync(1);
            await Assert.ThrowsAsync<ProductNotFoundException>(async () => { await ticketManager.RemoveProductAsync(1, -1, 1); });
        }

        [Fact]
        public async Task CreatePackageTicket()
        {
            var customerStore = new CustomerStore();
            var productStore = new ProductStore();
            var ticketStore = new TicketStore();
            var ticketManager = new TicketManager(ticketStore, customerStore, productStore);
            
            var id = await ticketManager.CreateAsync(1);
            await ticketManager.AddProductAsync(1, 3, 1);
            await ticketManager.AddProductAsync(1, 6, 1);
            await ticketManager.AddProductAsync(1, 7, 1);
            await ticketManager.AddProductAsync(1, 8, 1);
            
            var ticket = await ticketManager.GetAsync(1);
            Assert.Equal(10, ticket.TotalAmount);
        }
    }
}