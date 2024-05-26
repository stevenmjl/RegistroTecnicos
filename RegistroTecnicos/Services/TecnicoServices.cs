using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services
{
    public class TecnicosServices
    {
        private readonly Contexto _context;
        public TecnicosServices(Contexto contexto)
        {
            _context = contexto;
        }

        private async Task<bool> Existe(int tecnicoId)
        {
            return await _context.Tecnico
                .AnyAsync(t => t.TecnicoId ==  tecnicoId);
        }

        private async Task<bool> Insertar(Tecnicos tecnico)
        {
            _context.Tecnico.Add(tecnico);
            return await _context
                .SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Tecnicos tecnicos)
        {
            _context.Update(tecnicos);
            return await _context
                .SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(Tecnicos tecnicos)
        {
            if (!await Existe(tecnicos.TecnicoId))
                return await Insertar(tecnicos);
            else
                return await Modificar(tecnicos);

        }

        public async Task<bool> Eliminar (int id)
        {
            var tecnicos = await _context.Tecnico
                .Where(t => t.TecnicoId == id)
                .ExecuteDeleteAsync();
            return tecnicos > 0;
        }

        public async Task<Tecnicos?> Buscar(int id)
        {
            return await _context.Tecnico
                .AsNoTracking()
                .FirstOrDefaultAsync(t =>  t.TecnicoId == id);
        }

        public async Task<List<Tecnicos>> Listar(Expression<Func<Tecnicos, bool>> criterio)
        {
            return await _context.Tecnico
                .AsNoTracking()
                .Where(criterio)
                .ToListAsync();
        }
    }
}
