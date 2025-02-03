using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;
public class Tickets
{
    [Key]
    public int TicketId { get; set; }

    [Required(ErrorMessage = "Debe especificar la fecha.")]
    [DataType(DataType.Date)]
    public DateTime Fecha { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Debe establecer una prioridad.")]
    [Range(1, 5, ErrorMessage = "La prioridad debe estar entre 1 y 5.")]
    public int Prioridad { get; set; }

    [Required(ErrorMessage = "Debe asociar un cliente.")]
    [ForeignKey("ClienteId")]
    public int ClienteId { get; set; }
    public Clientes? Cliente { get; set; }

    [Required(ErrorMessage = "Debe proporcionar un asunto.")]
    [MaxLength(100, ErrorMessage = "El asunto no puede tener más de 100 caracteres.")]
    public string? Asunto { get; set; }

    [Required(ErrorMessage = "Debe proporcionar una descripción.")]
    [MaxLength(300, ErrorMessage = "La descripción no puede tener más de 300 caracteres.")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "Debe especificar el tiempo invertido.")]
    [Range(0.1, 1000, ErrorMessage = "El tiempo invertido debe estar entre 0.1 y 1000 horas.")]
    public double TiempoInvertido { get; set; }

    [Required(ErrorMessage = "Debe asociar un técnico.")]
    [ForeignKey("TecnicoId")]
    public int TecnicoId { get; set; }
    public Tecnicos? Tecnico { get; set; }
}