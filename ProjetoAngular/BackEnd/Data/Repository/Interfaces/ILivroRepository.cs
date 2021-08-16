using System.Threading.Tasks;
using BackEnd.Models;
using System.Collections.Generic;

namespace BackEnd.Data.Repository.Interfaces
{
    public interface ILivroRepository
    {
        Task<IEnumerable<Livro>> ObterTodos(bool incluirSegmento, 
                                            bool incluirMarca,
                                            bool incluirAutor,
                                            bool incluirClassificacao,
                                            bool incluirGenero);

        Task<Livro> ObterPeloLivroId(int livroId, bool incluirSegmento, 
                                                  bool incluirMarca,
                                                  bool incluirAutor,
                                                  bool incluirClassificacao,
                                                  bool incluirGenero); 
    }
}