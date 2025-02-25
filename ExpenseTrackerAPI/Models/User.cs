using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Models
{
    public class User
    {
        /// <summary>
        /// Id of the expense
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Code of the User
        /// </summary>
        [Required]
        public required string Code { get; set; }

        /// <summary>
        /// Code of the User
        /// </summary>
        [Required]
        public required string Username { get; set; }

        /// <summary>
        /// Code of the User
        /// </summary>
        [Required]
        public required string Password { get; set; }

        /// <summary>
        /// date of when the expense was created on the database
        /// </summary>
        [Required]
        public required DateTime DateOfCreation { get; set; } = DateTime.Now;

    }
}
