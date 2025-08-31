using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerAPI.Models
{
    public class Currency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }        // e.g. "US Dollar"
        [Required]
        public string Acronym { get; set; }     // e.g. "USD"
        [Required]
        public string Symbol { get; set; }      // e.g. "$"
        [Required]
        public int DecimalPlaces { get; set; }  // e.g. 2
        [Required]
        public string CultureCode { get; set; } // e.g. "en-US"

        // For conversions
        public decimal? ExchangeRateToBase { get; set; }

        // Dates
        [Required]
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        public DateTime? LastUpdated { get; set; }

        // Flags
        [DefaultValue(false)]
        public bool IsDefault { get; set; }
        [DefaultValue(false)]
        public bool IsCrypto { get; set; }
        public string? Country { get; set; } // Country where the currency is common
    }
}
