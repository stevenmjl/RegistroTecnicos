using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class CiudadesService(IDbContextFactory<Contexto> DbFactory)
{
    private async Task<bool> Existe(int ciudadId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Ciudades
            .AnyAsync(c => c.CiudadId == ciudadId);
    }

    public async Task<bool> Existe(int ciudadId, string? nombre)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Ciudades
            .AnyAsync(c => c.CiudadId != ciudadId
            && (c.NombreCiudad.ToLower() == nombre.ToLower()));
    }

    private async Task<bool> Insertar(Ciudades ciudad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Ciudades.Add(ciudad);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Ciudades ciudad)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(ciudad);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Ciudades ciudad)
    {
        if (!await Existe(ciudad.CiudadId))
        {
            return await Insertar(ciudad);
        }
        else
        {
            return await Modificar(ciudad);
        }
    }

    public async Task<Ciudades?> Buscar(int ciudadId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Ciudades
            .FirstOrDefaultAsync(c => c.CiudadId == ciudadId);
    }

    public async Task<bool> Eliminar(int ciudadId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Ciudades
            .AsNoTracking()
            .Where(c => c.CiudadId == ciudadId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Ciudades>> Listar(Expression<Func<Ciudades, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Ciudades
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<int> UltimoId()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var ultimaCiudad = await contexto.Ciudades
            .OrderByDescending(c => c.CiudadId)
            .FirstOrDefaultAsync();
        return ultimaCiudad != null ? ultimaCiudad.CiudadId : 0;
    }
}
