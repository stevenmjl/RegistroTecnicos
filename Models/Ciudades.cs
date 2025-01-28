using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Ciudades
{
    [Key]
    public int CiudadId { get; set; }

    [Required(ErrorMessage = "Debe agregar el nombre de la ciudad.")]
    [RegularExpression(@"^[a-zA-ZñÑáéíóúÁÉÍÓÚàâçèéêëîïôûùüÿÀÂÇÈÉÊËÎÏÔÛÙÜŸ\s]+$",
        ErrorMessage = "El nombre de la ciudad no debe contener números ni caracteres especiales.")]
    [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
    public string? NombreCiudad { get; set; }
}