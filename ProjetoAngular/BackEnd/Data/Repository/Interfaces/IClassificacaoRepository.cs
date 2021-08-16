using System.Threading.Tasks;
using BackEnd.Models;
using System.Collections.Generic;

namespace BackEnd.Data.Repository.Interfaces
{
    public interface IClassificacaoRepository
    {
        Task<IEnumerable<Classificacao>> ObterTodos(bool incluirLivro);
        Task<IEnumerable<Classificacao>> ObterTodosPeloLivroId(int livroId, bool incluirLivro);
        Task<Classificacao> ObterPelaClassificacaoId(int classificacaoId, bool incluirLivro);
    }
}