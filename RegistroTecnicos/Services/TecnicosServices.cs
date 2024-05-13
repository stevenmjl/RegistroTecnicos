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

        public async Task<bool> Existe(int tecnicoId)
        {
            return await _context.Tecnico.AnyAsync(p => p.TecnicoId ==  tecnicoId);
        }

        public async Task<bool> Insertar(Tecnico tecnico)
        {
            _context.Tecnico.Add(tecnico);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Modificar(Tecnico tecnicos)
        {
            _context.Update(tecnicos);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(Tecnico tecnicos)
        {
            if (!await Existe(tecnicos.TecnicoId))
                return await Insertar(tecnicos);
            else
                return await Modificar(tecnicos);

        }

        public async Task<bool> Eliminar (int id)
        {
            var tecnicos = await _context.Tecnico.
                Where(p => p.TecnicoId == id).ExecuteDeleteAsync();
            return tecnicos > 0;
        }

        public async Task<Tecnico?> Buscar(int id)
        {
            return await _context.Tecnico
                .AsNoTracking()
                .FirstOrDefaultAsync(p =>  p.TecnicoId == id);
        }

        public List<Tecnico> Listar(Expression<Func<Tecnico, bool>> criterio)
        {
            return _context.Tecnico
                .AsNoTracking()
                .Where(criterio)
                .ToList();
        }
    }
}
