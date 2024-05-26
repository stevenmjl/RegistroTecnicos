using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Tecnicos
{
    [Key]
    public int TecnicoId { get; set; }

    [Required(ErrorMessage = "Este campo debe ser llenado con los datos requeridos")]
    [RegularExpression(@"[a-zA-ZñÑ\s]+$", ErrorMessage = "Este campo no debe contener caracteres especiales")]
    public string? Nombres { get; set; }

    [Required(ErrorMessage = "Este campo debe ser llenado con los datos requeridos")]
    [Range(0.1, 100000000, ErrorMessage = "Ingrese un sueldo mayor a 0 y menor a 100,000,000")]
    public decimal SueldoHora { get; set; }
}
