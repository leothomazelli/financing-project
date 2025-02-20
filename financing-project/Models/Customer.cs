using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace financing_project.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [RegularExpression("([0-9]{2}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[\\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[-]?[0-9]{2})", ErrorMessage = "CPF inválido.")]
        public string Cpf { get; set; }
        public string Name { get; set; }
        public string Uf { get; set; }
        public string CellPhone { get; set; }
        [ForeignKey("Cpf")]
        [JsonIgnore]
        public virtual ICollection<Financing>? Financings { get; set; }
    }
}
