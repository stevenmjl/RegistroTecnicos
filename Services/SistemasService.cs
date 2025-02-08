using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class SistemasService(IDbContextFactory<Contexto> DbFactory)
{
    private async Task<bool> Existe(int sistemaId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas
            .AnyAsync(s => s.SistemaId == sistemaId);
    }

    public async Task<bool> Existe(int sistemaId, string? descripcion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas
            .AnyAsync(s => s.SistemaId != sistemaId
            && s.Descripcion.ToLower() == descripcion.ToLower());
    }

    private async Task<bool> Insertar(Sistemas sistema)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Sistemas.Add(sistema);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Sistemas sistema)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(sistema);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Sistemas sistema)
    {
        if (!await Existe(sistema.SistemaId))
        {
            return await Insertar(sistema);
        }
        else
        {
            return await Modificar(sistema);
        }
    }

    public async Task<Sistemas?> Buscar(int sistemaId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas
            .FirstOrDefaultAsync(s => s.SistemaId == sistemaId);
    }

    public async Task<bool> Eliminar(int sistemaId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas
            .AsNoTracking()
            .Where(s => s.SistemaId == sistemaId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Sistemas>> Listar(Expression<Func<Sistemas, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Sistemas
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<int> UltimoId()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var ultimoSistema = await contexto.Sistemas
            .OrderByDescending(s => s.SistemaId)
            .FirstOrDefaultAsync();
        return ultimoSistema != null ? ultimoSistema.SistemaId : 0;
    }
}