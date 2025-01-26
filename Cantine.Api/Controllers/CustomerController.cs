using System;
using System.Threading.Tasks;
using Cantine.Exceptions;
using Cantine.Interfaces.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cantine.Api.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManager _customerManager;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerManager customerManager, ILogger<CustomerController> logger)
        {
            _customerManager = customerManager;
            _logger = logger;
        }

        [HttpGet("customers")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _customerManager.GetAllAsync());
        }

        [HttpGet("customers/{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var customer = await _customerManager.GetByIdAsync(id);
                return Ok(customer);
            }
            catch (CustomerNotFoundException)
            {
                // Do nothing, just catch
            }
            return BadRequest();
        }

        [HttpPost("customers/{id:int}/purse/daily")]
        public async Task<ActionResult> AddDailyFunds(int id)
        {
            try
            {
                var dailyFunds = new Random().Next(15, 50);
                if (await _customerManager.AddFundsAsync(id, dailyFunds))
                    return Ok(dailyFunds);
            }
            catch (CustomerNotFoundException)
            {
                // Do nothing, just catch
            }
            return BadRequest();
        }
    }
}
