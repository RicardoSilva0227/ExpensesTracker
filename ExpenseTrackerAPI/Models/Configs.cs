using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Models
{
    public class Configs
    {
        /// <summary>
        /// Id of the expense
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Id of the currency
        /// </summary>
        public int CurrencyId { get; set; }
        /// <summary>
        /// location path for the invoice storage
        /// </summary>
        public string InvoicePath { get; set; }

    }
}
