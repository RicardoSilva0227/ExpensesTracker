using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Models.Dto
{
    public class ExpenseTypeDto
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string? Type { get; set; }
        public string? Description { get; set; }

    }
}
