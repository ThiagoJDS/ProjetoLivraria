using System.Threading.Tasks;
using BackEnd.Models;
using System.Collections.Generic;

namespace BackEnd.Data.Repository.Interfaces
{
    public interface IMarcaRepository
    {
        Task<IEnumerable<Marca>> ObterTodos(bool incluirLivro);
        Task<IEnumerable<Marca>> ObterTodosPeloLivroId(int livroId, bool incluirLivro);
        Task<Marca> ObterPelaMarcaId(int marcaId, bool incluirLivro);
    }
}