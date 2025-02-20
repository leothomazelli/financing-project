using financing_project.Enums;
using System.ComponentModel.DataAnnotations;

namespace financing_project.Models
{
    public class RequestFinancing
    {
        public float Value { get; set; }
        public FinancingTypeEnum Type { get; set; }
        public int TotalInstallments { get; set; }
        public DateTime FirstExpirationDate { get; set; }
        [RegularExpression("([0-9]{2}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[\\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[-]?[0-9]{2})", ErrorMessage = "CPF inválido.")]
        public string Cpf { get; set; }
    }
}
