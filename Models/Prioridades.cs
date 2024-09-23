using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class Prioridades
{
    [Key]
    public int PrioridadId { get; set; }

    [Required(ErrorMessage = "La descripción es obligatoria.")]
    [StringLength(100, ErrorMessage = "La descripción no debe exceder los 100 caracteres.")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "Debe especificar el tiempo en horas.")]
    [Range(1, 1000, ErrorMessage = "El tiempo debe estar entre 1 y 1000 horas.")]
    public int Tiempo { get; set; }
}
