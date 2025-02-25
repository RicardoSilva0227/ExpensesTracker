using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Models
{
    public class ExpenseType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <summary>
        /// Id of the expense Type
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Code of the ExpenseType
        /// </summary>
        [Required]
        public required string Code { get; set; }

        /// <summary>
        /// Description of the Expense Type
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Icon of the ExpenseType
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// date of when the expense was created on the database
        /// </summary>
        [Required]
        public required DateTime DateOfCreation { get; set; } = DateTime.Now;
    }
}
