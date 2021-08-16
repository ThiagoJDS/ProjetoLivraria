using System.Threading.Tasks;
using BackEnd.Models;
using System.Collections.Generic;

namespace BackEnd.Data.Repository.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ObterTodos();

        Task<Cliente> ObterPeloClienteId(int clienteId);
    }
}