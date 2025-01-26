using System;
using System.Threading.Tasks;
using Cantine.Exceptions;
using Cantine.Interfaces.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cantine.Api.Controllers
{
    [ApiController]
    public class CantineController : ControllerBase
    {
        private readonly ITicketManager _ticketManager;
        private readonly ILogger<CantineController> _logger;

        public CantineController(ITicketManager ticketManager, ILogger<CantineController> logger)
        {
            _ticketManager = ticketManager;
            _logger = logger;
        }

        [HttpGet("cantine/tickets/{id:int}")]
        public async Task<IActionResult> GetTicket(int id)
        {
            try
            {
                return Ok(await _ticketManager.GetAsync(id));
            }
            catch (TicketNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("cantine/tickets/customer/{customerId:int}")]
        public async Task<IActionResult> CreateTicket(int customerId)
        {
            return Ok(await _ticketManager.CreateAsync(customerId));
        }

        [HttpPut("cantine/tickets/{id:int}/products/{productId:int}/add/{quantity}")]
        public async Task<IActionResult> AddProduct(int id, int productId, int quantity)
        {
            try
            {
                if(await _ticketManager.AddProductAsync(id, productId, quantity))
                    return Ok();
            }
            catch (ProductNotFoundException)
            {
                return NotFound();
            }
            catch (TicketNotFoundException)
            {
                return NotFound();
            }
            catch (TicketAlreadyPaidException e)
            {
                ModelState.AddModelError("ticket", e.Message);
            }
            catch (ArgumentOutOfRangeException)
            {
                ModelState.AddModelError("quantity", "The quantity should be positive");
            }

            return BadRequest();
        }

        [HttpDelete("cantine/tickets/{id:int}/products/{productId:int}/add/{quantity}")]
        public async Task<IActionResult> RemoveProduct(int id, int productId, int quantity)
        {
            try
            {
                if(await _ticketManager.RemoveProductAsync(id, productId, quantity))
                    return Ok();
            }
            catch (ProductNotFoundException)
            {
                return NotFound();
            }
            catch (TicketNotFoundException)
            {
                return NotFound();
            }
            catch (TicketAlreadyPaidException e)
            {
                ModelState.AddModelError("ticket", e.Message);
            }
            catch (ArgumentOutOfRangeException)
            {
                ModelState.AddModelError("quantity", "The quantity should be negative");
            }

            return BadRequest();
        }

        [HttpPost("cantine/tickets/{id:int}/pay")]
        public async Task<IActionResult> PayTicket(int id)
        {
            try
            {
                if (await _ticketManager.PayAsync(id))
                    return Ok();
            }
            catch (TicketAlreadyPaidException e)
            {
                ModelState.AddModelError("ticket", e.Message);
            }
            catch (InsufficientFundsException e)
            {
                ModelState.AddModelError("customer", e.Message);
            }
            catch (TicketNotFoundException)
            {
                return NotFound();
            }

            return BadRequest(ModelState);
        }
    }
}
