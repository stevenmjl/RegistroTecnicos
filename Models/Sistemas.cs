using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Sistemas
{
    [Key]
    public int SistemaId { get; set; }

    [Required(ErrorMessage = "Debe agregar una descripción del sistema.")]
    [MaxLength(200, ErrorMessage = "La descripción no puede tener más de 200 caracteres.")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "Debe especificar la complejidad.")]
    [Range(1, 10, ErrorMessage = "La complejidad debe estar entre 1 y 10.")]
    public int Complejidad { get; set; }
}