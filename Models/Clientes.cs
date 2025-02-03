using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;

public class Clientes
{
    [Key]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "Debe especificar la fecha de ingreso.")]
    [DataType(DataType.Date)]
    public DateTime FechaIngreso { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Se debe agregar el nombre completo.")]
    [RegularExpression(@"^[a-zA-ZñÑáéíóúÁÉÍÓÚàâçèéêëîïôûùüÿÀÂÇÈÉÊËÎÏÔÛÙÜŸ\s]+$",
    ErrorMessage = "Este campo no debe contener números ni caracteres especiales.")]
    [MaxLength(50, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
    public string? Nombres { get; set; }

    [Required(ErrorMessage = "Debe agregar una dirección.")]
    [MaxLength(200, ErrorMessage = "La dirección no puede tener más de 200 caracteres.")]
    public string? Direccion { get; set; }

    [Required(ErrorMessage = "Debe agregar el RNC.")]
    [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "El RNC debe tener exactamente 9 dígitos.")]
    public string? Rnc { get; set; }

    [Required(ErrorMessage = "Debe establecer un límite de crédito.")]
    [Range(0.01, 1000000, ErrorMessage = "El límite de crédito debe estar entre 0.01 y 1,000,000.")]
    public double LimiteCredito { get; set; }

    [Required(ErrorMessage = "Debe asociar un técnico.")]
    [ForeignKey("TecnicoId")]
    public int TecnicoId { get; set; }
    public Tecnicos? Tecnico { get; set; }

    [Required(ErrorMessage = "Debe asociar una ciudad.")]
    [ForeignKey("CiudadId")]
    public int CiudadId { get; set; }
    public Ciudades? Ciudad { get; set; }
}