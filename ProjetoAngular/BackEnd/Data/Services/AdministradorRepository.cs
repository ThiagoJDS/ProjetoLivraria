using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data.Repository.Interfaces;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data.Services
{
    public class AdministradorRepository : IAdministradorRepository
    {
        private readonly DataContext _context;
        public AdministradorRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Administrador> ObterPeloAdministradorId(int administradorId)
        {
            IQueryable<Administrador> consulta = _context.Administrador;

            consulta = consulta.AsNoTracking()
                               .OrderBy(a => a.id)
                               .Where(a => a.id == administradorId);

            return await consulta.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Administrador>> ObterTodos()
        {
            IQueryable<Administrador> consulta = _context.Administrador;

            consulta = consulta.AsNoTracking().OrderBy(a => a.id);

            return await consulta.ToArrayAsync();
        }
    }
}