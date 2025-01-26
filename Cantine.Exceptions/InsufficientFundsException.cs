namespace Cantine.Exceptions
{
    /// <summary>
    /// This class describes exception triggers when the customer has not enough funds to pay the ticket
    /// </summary>
    public class InsufficientFundsException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public InsufficientFundsException()
        : base("There are insufficient funds to pay the ticket.")
        {
            
        }
    }
}
