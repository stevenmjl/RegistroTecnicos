using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Tecnicos
{
    [Key]
    public int TecnicoId { get; set; }

    [Required(ErrorMessage = "Se debe agregar el nombre completo.")]
    [RegularExpression(@"^[a-zA-ZñÑáéíóúÁÉÍÓÚàâçèéêëîïôûùüÿÀÂÇÈÉÊËÎÏÔÛÙÜŸ\s]+$",
    ErrorMessage = "Este campo no debe contener números ni caracteres especiales.")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "Debe tener un sueldo correcto.")]
    [Range(0.1, 100000000, ErrorMessage = "Ingrese un sueldo mayor a 0 y menor a 100,000,000")]
    public decimal SueldoHora { get; set; }
}