using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TiposTecnicoService
{
    // Variables
    private readonly Contexto _contexto;
    public TiposTecnicoService(Contexto contexto)
    {
        _contexto = contexto;
    }

    // Métodos del servicio
    private async Task<bool> Existe(int tipoTecnicoId)
    {
        return await _contexto.TiposTecnicos
            .AnyAsync(t => t.TipoTecnicoId == tipoTecnicoId);
    }
    public async Task<bool> Existe(int tipoTecnicoId, string? descripcion)
    {
        return await _contexto.TiposTecnicos
            .AnyAsync(t => t.TipoTecnicoId != tipoTecnicoId && t.Descripcion.ToLower().Equals(descripcion.ToLower()));
    }
    public async Task<bool> Guardar(TiposTecnicos tipoTecnico)
    {
        if (!await Existe(tipoTecnico.TipoTecnicoId))
            return await Insertar(tipoTecnico);
        else
            return await Modificar(tipoTecnico);

    }
    private async Task<bool> Insertar(TiposTecnicos tipoTecnico)
    {
        _contexto.TiposTecnicos.Add(tipoTecnico);
        return await _contexto
            .SaveChangesAsync() > 0;
    }
    private async Task<bool> Modificar(TiposTecnicos tipoTecnico)
    {
        _contexto.Update(tipoTecnico);
        return await _contexto
            .SaveChangesAsync() > 0;
    }
    public async Task<bool> Eliminar(int tipoTecnicoId)
    {
        var tipoTecnico = await _contexto.TiposTecnicos
            .Where(t => t.TipoTecnicoId == tipoTecnicoId)
            .ExecuteDeleteAsync();
        return tipoTecnico > 0;
    }
    public async Task<TiposTecnicos?> BuscarId(int tipoTecnicoId)
    {
        return await _contexto.TiposTecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TipoTecnicoId == tipoTecnicoId);
    }

    public async Task<String?> BuscarDescripcion(int tipoTecnicoId)
    {
        var descripcion = await _contexto.TiposTecnicos
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TipoTecnicoId == tipoTecnicoId);

        return descripcion?.Descripcion;
    }

    public async Task<List<TiposTecnicos>> Listar(Expression<Func<TiposTecnicos, bool>> criterio)
    {
        return await _contexto.TiposTecnicos
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    // Usado en la visualización de la tabla tipos técnicos
    public async Task<int> UltimoId()
    {
        var ultimoTipoTecnico = await _contexto.TiposTecnicos
            .OrderByDescending(t => t.TipoTecnicoId)
            .FirstOrDefaultAsync();
        return ultimoTipoTecnico != null ? ultimoTipoTecnico.TipoTecnicoId : 0;
    }

    // Para evitar errores de rasteo de entidad
    public void DesvincularLocal(int tipoTecnicoId)
    {
        var local = _contexto.Set<TiposTecnicos>().Local.FirstOrDefault(entry => entry
        .TipoTecnicoId == tipoTecnicoId);
        if (local != null)
        {
            _contexto.Entry(local).State = EntityState.Detached;
        }
    }
}