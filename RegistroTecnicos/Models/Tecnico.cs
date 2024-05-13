using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models
{
    public class Tecnico
    {
        [Key]
        public int TecnicoId { get; set; }
        public string? Nombres { get; set; }
        public float SueldoHora { get; set; }
    }
}