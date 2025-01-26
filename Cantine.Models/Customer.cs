using System.Text.Json.Serialization;

namespace Cantine.Models
{
    /// <summary>
    /// This class describes Customer
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// The identifier
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// The first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The customer type
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CustomerTypeEnum Type { get; set; }

        /// <summary>
        /// The purse amount
        /// </summary>
        public double PurseAmount { get; set; }
    }
}
