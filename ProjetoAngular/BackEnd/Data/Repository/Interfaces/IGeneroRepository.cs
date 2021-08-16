using System.Threading.Tasks;
using BackEnd.Models;
using System.Collections.Generic;

namespace BackEnd.Data.Repository.Interfaces
{
    public interface IGeneroRepository
    {
        Task<IEnumerable<Genero>> ObterTodos(bool incluirLivro);
        Task<IEnumerable<Genero>> ObterTodosPeloLivroId(int livroId, bool incluirLivro);
        Task<Genero> ObterPeloGeneroId(int generoId, bool incluirLivro); 
    }
}