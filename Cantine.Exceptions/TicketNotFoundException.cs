using Cantine.Models;

namespace Cantine.Exceptions
{
    /// <summary>
    /// This class describes the base exception triggers when a ticket was not found
    /// </summary>
    public class TicketNotFoundException : ItemNotFoundException<Ticket>
    {
    }
}
