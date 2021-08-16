using System.Threading.Tasks;
using BackEnd.Models;
using System.Collections.Generic;

namespace BackEnd.Data.Repository.Interfaces
{
    public interface ISegmentoRepository
    {
        Task<IEnumerable<Segmento>> ObterTodos(bool incluirLivro);
        Task<IEnumerable<Segmento>> ObterTodosPeloLivroId(int livroId, bool incluirLivro);
        Task<Segmento> ObterPeloSegmentoId(int segmentoId, bool incluirLivro); 
    }
}