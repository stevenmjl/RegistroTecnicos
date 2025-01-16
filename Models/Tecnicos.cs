using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Tecnicos
{
    [Key]
    public int TecnicoId { get; set; }

    [Required(ErrorMessage = "Se debe agregar el nombre completo.")]
    [RegularExpression(@"^[a-zA-ZñÑáéíóúÁÉÍÓÚàâçèéêëîïôûùüÿÀÂÇÈÉÊËÎÏÔÛÙÜŸ\s]+$",
    ErrorMessage = "Este campo no debe contener números ni caracteres especiales.")]
    public string? Nombres { get; set; }

    [Required(ErrorMessage = "Debe tener un sueldo válido.")]
    [Range(0.01, 10000000, ErrorMessage = "Ingrese un sueldo mayor a 0 y menor a 10,000,000")]
    public double SueldoHora { get; set; }

}