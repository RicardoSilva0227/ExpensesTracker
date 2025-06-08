using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Models
{
    public class WalletEntries
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Wallet Reference.
        /// </summary>
        public int WalletId { get; set; }
        /// <summary>
        /// Amount of the transaction (positive for income, negative for expense).
        /// </summary>
        [Required]
        public decimal Amount { get; set; }

        /// <summary>
        /// Type of transaction (e.g., income, expense).
        /// </summary>
        [Required]
        public int TypeId { get; set; }

        /// <summary>
        /// Optional description or note about the entry.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Date and time when this entry was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Expense that caused this entry.
        /// </summary>
        public int ExpenseId { get; set; }


        public ExpenseType ExpenseType { get; set; }
        public Wallet wallet { get; set; }
        public Expense expense { get; set; }

    }
}
