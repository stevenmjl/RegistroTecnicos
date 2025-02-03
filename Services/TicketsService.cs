using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services
{
    public class TicketsService(IDbContextFactory<Contexto> DbFactory)
    {
        private async Task<bool> Existe(int ticketId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tickets
                .AnyAsync(t => t.TicketId == ticketId);
        }

        public async Task<bool> Existe(int ticketId, string? asunto)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tickets
                .AnyAsync(t => t.TicketId != ticketId
                && (t.Asunto.ToLower() == asunto.ToLower()));
        }

        private async Task<bool> Insertar(Tickets ticket)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Tickets.Add(ticket);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Tickets ticket)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Update(ticket);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(Tickets ticket)
        {
            if (!await Existe(ticket.TicketId))
            {
                return await Insertar(ticket);
            }
            else
            {
                return await Modificar(ticket);
            }
        }

        public async Task<Tickets?> Buscar(int ticketId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tickets
                .FirstOrDefaultAsync(t => t.TicketId == ticketId);
        }

        public async Task<bool> Eliminar(int ticketId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tickets
                .AsNoTracking()
                .Where(t => t.TicketId == ticketId)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<List<Tickets>> Listar(Expression<Func<Tickets, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tickets
                .Include(t => t.Cliente) // Incluye los datos del cliente
                .Include(t => t.Tecnico) // Incluye los datos del técnico
                .Where(criterio)
                .ToListAsync();
        }

        public async Task<int> UltimoId()
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var ultimoTicket = await contexto.Tickets
                .OrderByDescending(t => t.TicketId)
                .FirstOrDefaultAsync();
            return ultimoTicket != null ? ultimoTicket.TicketId : 0;
        }
    }
}