using financing_project.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace financing_project.Models
{
    public class Financing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(24)")]
        public FinancingTypeEnum Type { get; set; }
        public float Total { get; set; }
        public DateTime LastExpirationDate { get; set; }
        [ForeignKey("Customer")]
        [RegularExpression("([0-9]{2}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[\\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[-]?[0-9]{2})", ErrorMessage = "CPF inválido.")]
        public string Cpf { get; set; }
        [JsonIgnore]
        public virtual Customer? Customer { get; set; }
        [ForeignKey("FinancingId")]
        [JsonIgnore]
        public virtual ICollection<Installment>? Installments { get; set; }
    }
}
