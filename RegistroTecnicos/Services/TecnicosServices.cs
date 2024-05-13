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
    }
}
