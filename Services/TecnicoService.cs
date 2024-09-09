using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TecnicoService
{
    // Variables
    private readonly Contexto _contexto;
    public TecnicoService(Contexto contexto)
    {
        _contexto = contexto;
    }

    // Métodos del servicio
    private async Task<bool> Existe(int tecnicoId)
    {
        return await _contexto.Tecnicos
            .AnyAsync(t => t.TecnicoId == tecnicoId);
    }
    public async Task<bool> Existe(int tecnicoId, string? nombres)
    {
        return await _contexto.Tecnicos
            .AnyAsync(t => t.TecnicoId != tecnicoId && t.Nombres.ToLower().Equals(nombres.ToLower()));
    }
    public async Task<bool> Guardar(Tecnicos tecnico)
    {
        if (!await Existe(tecnico.TecnicoId))
            return await Insertar(tecnico);
        else
            return await Modificar(tecnico);

    }
    private async Task<bool> Insertar(Tecnicos tecnico)
    {
        _contexto.Tecnicos.Add(tecnico);
        return await _contexto
            .SaveChangesAsync() > 0;
    }
    private async Task<bool> Modificar(Tecnicos tecnico)
    {
        _contexto.Update(tecnico);
        return await _contexto
            .SaveChangesAsync() > 0;
    }
    public async Task<bool> Eliminar(int tecnicoId)
    {
        var tecnico = await _contexto.Tecnicos
            .Where(t => t.TecnicoId == tecnicoId)
            .ExecuteDeleteAsync();
        return tecnico > 0;
    }
    public async Task<Tecnicos?> BuscarId(int tecnicoId)
    {
        return await _contexto.Tecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TecnicoId == tecnicoId);
    }
    public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
        return await _contexto.Tecnicos
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    // Usado en la visualización de la tabla técnicos
    public async Task<int> UltimoId()
    {
        var ultimoTecnico = await _contexto.Tecnicos
            .OrderByDescending(t => t.TecnicoId)
            .FirstOrDefaultAsync();
        return ultimoTecnico != null ? ultimoTecnico.TecnicoId : 0;
    }

    // Para evitar errores de rasteo de entidad
    public void DesvincularLocal(int tecnicoId)
    {
        var local = _contexto.Set<Tecnicos>().Local.FirstOrDefault(entry => entry.TecnicoId == tecnicoId);
        if (local != null)
        {
            _contexto.Entry(local).State = EntityState.Detached;
        }
    }
}