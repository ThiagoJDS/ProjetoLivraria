using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data.Repository.Interfaces;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;


namespace BackEnd.Data.Services
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DataContext _context;
        public ClienteRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Cliente> ObterPeloClienteId(int clienteId)
        {
            IQueryable<Cliente> consulta = _context.Cliente;

            consulta = consulta.AsNoTracking()
                               .OrderBy(c => c.id)
                               .Where(c => c.id == clienteId);

            return await consulta.FirstOrDefaultAsync();                   
        }

        public async Task<IEnumerable<Cliente>> ObterTodos()
        {
            IQueryable<Cliente> consulta = _context.Cliente;

            consulta = consulta.AsNoTracking().OrderBy(c => c.id);

            return await consulta.ToArrayAsync();
        }
    }
}