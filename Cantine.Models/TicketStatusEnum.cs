namespace Cantine.Models;

/// <summary>
/// This enumeration lists all the available ticket statuses
/// </summary>
public enum TicketStatusEnum
{
    /// <summary>
    /// Newly created
    /// </summary>
    New,
    /// <summary>
    /// Waiting payment when having one or more product
    /// </summary>
    WaitingPayment,
    /// <summary>
    /// Paid
    /// </summary>
    Paid
}