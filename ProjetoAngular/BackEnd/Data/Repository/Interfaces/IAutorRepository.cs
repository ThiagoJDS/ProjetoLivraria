using System.Threading.Tasks;
using BackEnd.Models;
using System.Collections.Generic;

namespace BackEnd.Data.Repository.Interfaces
{
    public interface IAutorRepository
    {
        Task<IEnumerable<Autor>> ObterTodos(bool incluirLivro);
        Task<IEnumerable<Autor>> ObterTodosPeloLivroId(int livroId, bool incluirLivro);
        Task<Autor> ObterPeloAutorId(int autorId, bool incluirLivro);
    }
}