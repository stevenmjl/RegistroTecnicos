using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class TecnicosService(IDbContextFactory<Contexto> DbFactory)
{
    private async Task<bool> Existe(int tecnicoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .AnyAsync(t => t.TecnicoId == tecnicoId);
    }

    public async Task<bool> Existe(int tecnicoId, string? nombres)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .AnyAsync(t => t.TecnicoId != tecnicoId && t.Nombres.ToLower().Equals(nombres.ToLower()));
    }

    private async Task<bool> Insertar(Tecnicos tecnico)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Tecnicos.Add(tecnico);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Tecnicos tecnico)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(tecnico);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Tecnicos tecnico)
    {
        if(!await Existe(tecnico.TecnicoId)){
            return await Insertar(tecnico);
        }
        else{
            return await Modificar(tecnico);
        }
    }

    public async Task<Tecnicos?> Buscar(int tecnicoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .FirstOrDefaultAsync(t => t.TecnicoId == tecnicoId);
    }

    public async Task<bool> Eliminar(int tecnicoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .AsNoTracking()
            .Where(t => t.TecnicoId == tecnicoId)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Tecnicos
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<int> UltimoId()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var ultimoTecnico = await contexto.Tecnicos
            .OrderByDescending(t => t.TecnicoId)
            .FirstOrDefaultAsync();
        return ultimoTecnico != null ? ultimoTecnico.TecnicoId : 0;
    }
    // Busca en el caché local, Desvincula la entidad del seguimiento si causa interferencia.
    public async Task DesvincularLocal(int tecnicoId)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var local = contexto.Set<Tecnicos>().Local.FirstOrDefault(l => l.TecnicoId == tecnicoId);
       
        if (local == null)
            return;
        contexto.Entry(local).State = EntityState.Detached;
    }
}
