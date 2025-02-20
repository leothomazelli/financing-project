using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace financing_project.Models
{
    public class Installment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }
        public int InstallmentNumber { get; set; }
        public float InstallmentValue { get; set; }
        public DateTime ExpirationDate { get; set; } = DateTime.Now;
        public DateTime PaymentDate { get; set; }
        [ForeignKey("Financing")]
        public int FinancingId { get; set; }
        [JsonIgnore]
        public virtual Financing? Financing { get; set; }
    }
}
