using System.Threading.Tasks;
using BackEnd.Models;
using System.Collections.Generic;

namespace BackEnd.Data.Repository.Interfaces
{
    public interface IAdministradorRepository
    {
        Task<IEnumerable<Administrador>> ObterTodos();

        Task<Administrador> ObterPeloAdministradorId(int administradorId);
    }
}