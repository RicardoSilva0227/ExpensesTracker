using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ExpenseTrackerAPI.Models
{
    public class Configs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // References
        [ForeignKey("Currency")]
        public int? DefaultCurrencyId { get; set; }
        public Currency? DefaultCurrency { get; set; }

        // FTP Settings
        [DefaultValue(false)]
        public bool UseFtp { get; set; } = false;

        [MaxLength(255)]
        public string? FtpServer { get; set; }

        [MaxLength(255)]
        public string? FtpUsername { get; set; }

        [MaxLength(255)]
        public string? FtpPassword { get; set; }

        public int? FtpPort { get; set; }

        // Email Settings
        [MaxLength(255)]
        public string? SmtpServer { get; set; }

        public int? SmtpPort { get; set; }

        [MaxLength(255)]
        public string? SmtpUsername { get; set; }

        [MaxLength(255)]
        public string? SmtpPassword { get; set; }

        // System Settings
        [DefaultValue("UTC")]
        [MaxLength(50)]
        public string Timezone { get; set; } = "UTC";

        [MaxLength(10)]
        public string? DateFormat { get; set; } // e.g. "dd/MM/yyyy"

        [DefaultValue(true)]
        public bool EnableMultiCurrency { get; set; } = true;

        [DefaultValue(false)]
        public bool EnableDiscounts { get; set; } = false;

        // Dates
        [Required]
        public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdated { get; set; }

    }
}
