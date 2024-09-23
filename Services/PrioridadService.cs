using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class PrioridadService
{
    // Variables
    private readonly Contexto _contexto;
    public PrioridadService(Contexto contexto)
    {
        _contexto = contexto;
    }

    // Métodos del servicio
    private async Task<bool> Existe(int prioridadId)
    {
        return await _contexto.Prioridades
            .AnyAsync(p => p.PrioridadId == prioridadId);
    }
    public async Task<bool> Existe(int prioridadId, string? descripcion)
    {
        return await _contexto.Prioridades
            .AnyAsync(p => p.PrioridadId != prioridadId && p.Descripcion.ToLower().Equals(descripcion.ToLower()));
    }
    public async Task<bool> Guardar(Prioridades prioridad)
    {
        if (!await Existe(prioridad.PrioridadId))
            return await Insertar(prioridad);
        else
            return await Modificar(prioridad);

    }
    private async Task<bool> Insertar(Prioridades prioridad)
    {
        _contexto.Prioridades.Add(prioridad);
        return await _contexto
            .SaveChangesAsync() > 0;
    }
    private async Task<bool> Modificar(Prioridades prioridad)
    {
        _contexto.Update(prioridad);
        return await _contexto
            .SaveChangesAsync() > 0;
    }
    public async Task<bool> Eliminar(int prioridadId)
    {
        var prioridad = await _contexto.Prioridades
            .Where(p => p.PrioridadId == prioridadId)
            .ExecuteDeleteAsync();
        return prioridad > 0;
    }
    public async Task<Prioridades?> BuscarId(int prioridadId)
    {
        return await _contexto.Prioridades
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PrioridadId == prioridadId);
    }

    public async Task<List<Prioridades>> Listar(Expression<Func<Prioridades, bool>> criterio)
    {
        return await _contexto.Prioridades
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    // Usado en la visualización de la tabla tipos técnicos
    public async Task<int> UltimoId()
    {
        var ultimaPrioridad = await _contexto.Prioridades
            .OrderByDescending(p => p.PrioridadId)
            .FirstOrDefaultAsync();
        return ultimaPrioridad != null ? ultimaPrioridad.PrioridadId : 0;
    }

    // Para evitar errores de rasteo de entidad
    public void DesvincularLocal(int prioridadId)
    {
        var local = _contexto.Set<Prioridades>().Local.FirstOrDefault(entry => entry
        .PrioridadId == prioridadId);
        if (local != null)
        {
            _contexto.Entry(local).State = EntityState.Detached;
        }
    }
}