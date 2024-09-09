using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicos.Models;

public class TiposTecnicos
{
    [Key]
    public int TipoTecnicoId { get; set; }

    [Required(ErrorMessage = "Debe aclarar un tipo de tecnico correcto.")]
    [RegularExpression(@"^[a-zA-ZñÑáéíóúÁÉÍÓÚàâçèéêëîïôûùüÿÀÂÇÈÉÊËÎÏÔÛÙÜŸ\s]+$", 
    ErrorMessage = "No debe contener caracteres especiales ni números")]
    public string? Descripcion { get; set; }
}