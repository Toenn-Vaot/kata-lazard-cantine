using System.Text.Json.Serialization;

namespace Cantine.Models;

public class Ticket
{
    /// <summary>
    /// The identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The customer identifier
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// The collection of items composed with product identifier and the quantity
    /// </summary>
    public Dictionary<int, int> Items { get; set; }

    /// <summary>
    /// The ticket status
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TicketStatusEnum Status { get; set; }

    /// <summary>
    /// The total amount
    /// </summary>
    public double TotalAmount { get; set; }
}