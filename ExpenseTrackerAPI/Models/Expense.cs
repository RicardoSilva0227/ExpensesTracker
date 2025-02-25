using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Models
{
  
    public class Expense
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <summary>
        /// Id of the expense
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Code of the invoice. It is created using the id, type of expense and date of emission
        /// </summary>
        [Required]
        public required string Code { get; set; }
        /// <summary>
        /// Date when the invoice was emitted
        /// </summary>
        public DateTime? DateOfEmission { get; set; }
        /// <summary>
        /// date of when the expense was created on the database
        /// </summary>
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        /// <summary>
        /// Type of expense (foreign Key with ExpenseType)
        /// </summary>
        [ForeignKey("ExpenseType")]
        public int? ExpenseTypeId { get; set; }
        public ExpenseType? ExpenseType { get; set; }

    }
}
