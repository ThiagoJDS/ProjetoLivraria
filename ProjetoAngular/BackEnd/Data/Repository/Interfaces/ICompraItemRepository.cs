using System.Threading.Tasks;
using BackEnd.Models;
using System.Collections.Generic;

namespace BackEnd.Data.Repository.Interfaces
{
    public interface ICompraItemRepository
    {
        Task<IEnumerable<CompraItem>> ObterTodos(bool incluirLivro, bool incluirCompra);
        Task<CompraItem> ObterPeloCompraItemId(int compraItemId, bool incluirLivro, bool incluirCompra);
    }
}