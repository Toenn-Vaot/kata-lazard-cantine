using Cantine.Models;

namespace Cantine.Exceptions;

/// <summary>
/// This class describes the base exception triggers when a customer was not found
/// </summary>
public class CustomerNotFoundException : ItemNotFoundException<Customer>
{
}

/// <summary>
/// This class describes the exception triggers when the ticket was already paid
/// </summary>
public class TicketAlreadyPaidException : Exception
{
    /// <summary>
    /// Constructor
    /// </summary>
    public TicketAlreadyPaidException() : base("The ticket was already paid")
    {

    }
}