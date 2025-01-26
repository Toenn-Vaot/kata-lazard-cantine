using Cantine.Exceptions;
using Cantine.Managers;
using Cantine.Models;
using Cantine.Stores;

namespace Cantine.Tests
{
    public class CustomerManagerTests
    {
        [Fact]
        public async Task CustomerXContractorNotExists()
        {
            var customerStore = new CustomerStore();
            var customerManager = new CustomerManager(customerStore);
            await Assert.ThrowsAsync<CustomerNotFoundException>(async () => { await customerManager.GetByIdAsync(99); });
        }

        [Fact]
        public async Task Customer1ContractorExists()
        {
            var customerStore = new CustomerStore();
            var customerManager = new CustomerManager(customerStore);
            var customer = await customerManager.GetByIdAsync(1);

            Assert.NotNull(customer);
            Assert.Equal("Peter", customer.FirstName);
            Assert.Equal("Aldrick", customer.LastName);
            Assert.Equal(CustomerTypeEnum.Contractor, customer.Type);
        }

        [Fact]
        public async Task Customer2EmployeeExists()
        {
            var customerStore = new CustomerStore();
            var customerManager = new CustomerManager(customerStore);
            var customer = await customerManager.GetByIdAsync(2);

            Assert.NotNull(customer);
            Assert.Equal("Gérard", customer.FirstName);
            Assert.Equal("Majax", customer.LastName);
            Assert.Equal(CustomerTypeEnum.Employee, customer.Type);
        }

        [Fact]
        public async Task Customer3EmployeeExists()
        {
            var customerStore = new CustomerStore();
            var customerManager = new CustomerManager(customerStore);
            var customer = await customerManager.GetByIdAsync(3);

            Assert.NotNull(customer);
            Assert.Equal("Gustave", customer.FirstName);
            Assert.Equal("Flaubert", customer.LastName);
            Assert.Equal(CustomerTypeEnum.Guest, customer.Type);
        }

        [Fact]
        public async Task Customer4TraineeExists()
        {
            var customerStore = new CustomerStore();
            var customerManager = new CustomerManager(customerStore);
            var customer = await customerManager.GetByIdAsync(4);

            Assert.NotNull(customer);
            Assert.Equal("Marc", customer.FirstName);
            Assert.Equal("Delage", customer.LastName);
            Assert.Equal(CustomerTypeEnum.Trainee, customer.Type);
        }

        [Fact]
        public async Task Customer5VipExists()
        {
            var customerStore = new CustomerStore();
            var customerManager = new CustomerManager(customerStore);
            var customer = await customerManager.GetByIdAsync(5);

            Assert.NotNull(customer);
            Assert.Equal("Jeff", customer.FirstName);
            Assert.Equal("Bezos", customer.LastName);
            Assert.Equal(CustomerTypeEnum.Vip, customer.Type);
        }

        [Fact]
        public async Task GetAllExists()
        {
            var customerStore = new CustomerStore();
            var customerManager = new CustomerManager(customerStore);
            
            var customers = await customerManager.GetAllAsync();

            Assert.Equal(5, customers.Count());
        }
        
        [Fact]
        public async Task Customer1AddPositiveAmountInPurse()
        {
            var customerStore = new CustomerStore();
            var customerManager = new CustomerManager(customerStore);
            var customer = await customerManager.GetByIdAsync(1);

            var positiveAmountToAdd = 10d;
            var previousPurseAmount = customer.PurseAmount;

            await customerManager.AddFundsAsync(1, positiveAmountToAdd);
            customer = await customerManager.GetByIdAsync(1);

            Assert.Equal(previousPurseAmount + positiveAmountToAdd, customer.PurseAmount);
        }
        
        [Fact]
        public async Task Customer1AddNegativeAmountInPurseThrowException()
        {
            var customerStore = new CustomerStore();
            var customerManager = new CustomerManager(customerStore);
            var negativeAmountToAdd = -10d;

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => { await customerManager.AddFundsAsync(1, negativeAmountToAdd); });
        }
        
        [Fact]
        public async Task Customer1RemoveNegativeAmountInPurse()
        {
            var customerStore = new CustomerStore();
            var customerManager = new CustomerManager(customerStore);
            var customer = await customerManager.GetByIdAsync(1);

            var negativeAmountToRemove = -10d;
            var previousPurseAmount = customer.PurseAmount;

            await customerManager.RemoveFundsAsync(1, negativeAmountToRemove);
            customer = await customerManager.GetByIdAsync(1);

            Assert.Equal(previousPurseAmount + negativeAmountToRemove, customer.PurseAmount);
        }
        
        [Fact]
        public async Task Customer1RemovePositiveAmountInPurseThrowException()
        {
            var customerStore = new CustomerStore();
            var customerManager = new CustomerManager(customerStore);
            var positiveAmountToRemove = 10d;

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => { await customerManager.RemoveFundsAsync(1, positiveAmountToRemove); });
        }
    }
}