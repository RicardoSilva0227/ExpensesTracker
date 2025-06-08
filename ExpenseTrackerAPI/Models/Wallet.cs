using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerAPI.Models
{
    public class Wallet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// User that this wallet belongs to
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Wallet balance
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        /// <summary>
        /// Currency code (e.g., USD, EUR) — to be implemented
        /// </summary>
        [MaxLength(3)]
        public string? Currency { get; set; }

        /// <summary>
        /// Last time this balance was updated
        /// </summary>
        public DateTime LastUpdated { get; set; }


        [NotMapped]
        public int TransactionsNumber { get; set; } = 0;

        [NotMapped]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalSpent { get; set; } = 0;

        public User User { get; set; }
    }
}
