using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data.Repository.Interfaces;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data.Services
{
    public class CompraRepository : ICompraRepository
    {
        private readonly DataContext _contexto;

        public CompraRepository(DataContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Compra>> ObterTodos(bool incluirCliente, bool incluirCompraItem )
        {
            IQueryable<Compra> consulta = _contexto.Compra;

            if(incluirCliente)
            {
                consulta = consulta.Include(a => a.cliente);
            }

            if(incluirCompraItem)
            {
                consulta = consulta.Include(a => a.CompraItems);
            }

            consulta = consulta.AsNoTracking().OrderBy(a => a.id);

            return await consulta.ToArrayAsync();
        }

        public async Task<Compra> ObterPelaCompraId(int compraId, bool incluirCliente, bool incluirCompraItem )
        {
            IQueryable<Compra> consulta = _contexto.Compra;

            if(incluirCliente)
            {
                consulta = consulta.Include(a => a.cliente);
            }

            if(incluirCompraItem)
            {
                consulta = consulta.Include(a => a.CompraItems);
            }

            consulta = consulta.AsNoTracking()
                               .OrderBy(a => a.id)
                               .Where(a => a.id == compraId);

            return await consulta.FirstOrDefaultAsync();
        }
    }
}