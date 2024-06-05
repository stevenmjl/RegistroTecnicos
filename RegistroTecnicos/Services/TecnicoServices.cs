using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services
{
    public class TecnicoServices
    {
        private readonly Contexto _context;
        public TecnicoServices(Contexto contexto)
        {
            _context = contexto;
        }
        private async Task<bool> Existe(int tecnicoId)
        {
            return await _context.Tecnico
                .AnyAsync(t => t.TecnicoId == tecnicoId);
        }
        public async Task<bool> Existe(int id, string nombre)
        {
            return await _context.Tecnico
                .AnyAsync(t => t.TecnicoId != id && t.Nombre.ToLower().Equals(nombre.ToLower()));
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
            _context.Tecnico.Add(tecnico);
            return await _context
                .SaveChangesAsync() > 0;
        }
        private async Task<bool> Modificar(Tecnicos tecnico)
        {
            _context.Update(tecnico);
            return await _context
                .SaveChangesAsync() > 0;
        }
        public async Task<bool> Eliminar(int tecnicoId)
        {
            var tecnico = await _context.Tecnico
                .Where(t => t.TecnicoId == tecnicoId)
                .ExecuteDeleteAsync();
            return tecnico > 0;
        }
        public async Task<Tecnicos?> Buscar(int tecnicoId)
        {
            return await _context.Tecnico
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TecnicoId == tecnicoId);
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
