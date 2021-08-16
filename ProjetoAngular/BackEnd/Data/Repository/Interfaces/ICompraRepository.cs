using System.Threading.Tasks;
using BackEnd.Models;
using System.Collections.Generic;

namespace BackEnd.Data.Repository.Interfaces
{
    public interface ICompraRepository
    {
        Task<IEnumerable<Compra>> ObterTodos(bool incluirCliente, bool incluirCompraItem);
        
        Task<Compra> ObterPelaCompraId(int compraId, bool incluirCliente,  bool incluirCompraItem);
    }
}