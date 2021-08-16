using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data.Repository.Interfaces;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data.Services
{
    public class CompraItemRepository : ICompraItemRepository
    {
        private readonly DataContext _contexto;

        public CompraItemRepository(DataContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<CompraItem>> ObterTodos(bool incluirLivro, bool incluirCompra)
        {
            IQueryable<CompraItem> consulta = _contexto.CompraItem;

            if(incluirLivro)
            {
                consulta = consulta.Include(a => a.livro);                          
            }
            if(incluirCompra)
            {
                consulta = consulta.Include(a => a.compra);                          
            }

            consulta = consulta.AsNoTracking().OrderBy(a => a.id);

            return await consulta.ToArrayAsync();
        }

        public async Task<CompraItem> ObterPeloCompraItemId(int compraItemId, bool incluirLivro, bool incluirCompra)
        {
            IQueryable<CompraItem> consulta = _contexto.CompraItem;

            if(incluirLivro){
                consulta = consulta.Include(a => a.livro);                      
            }

             if(incluirCompra){
                consulta = consulta.Include(a => a.compra);                      
            }

            consulta = consulta.AsNoTracking()
                               .OrderBy(a => a.id)
                               .Where(a => a.id == compraItemId);

            return await consulta.FirstOrDefaultAsync();                   
        }
    }
}