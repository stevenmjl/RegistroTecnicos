using RegistroTecnicos.Models;
using Microsoft.EntityFrameworkCore;

namespace RegistroTecnicos.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }
    public DbSet<Tecnicos> Tecnicos { get; set; }
    public DbSet<Clientes> Clientes { get; set; }
    public DbSet<Ciudades> Ciudades { get; set; }
    public DbSet<Tickets> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tickets>()
            .HasOne(t => t.Cliente)
            .WithMany()
            .HasForeignKey(t => t.ClienteId)
            .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada al eliminar un Cliente

        modelBuilder.Entity<Tickets>()
            .HasOne(t => t.Tecnico)
            .WithMany()
            .HasForeignKey(t => t.TecnicoId)
            .OnDelete(DeleteBehavior.NoAction); // Evitar eliminación en cascada al eliminar un Técnico

        base.OnModelCreating(modelBuilder);
    }
}