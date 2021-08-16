using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data.Repository.Interfaces;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data.Services
{
    public class ClassificacaoRepository : IClassificacaoRepository
    {
        private readonly DataContext _contexto;

        public ClassificacaoRepository(DataContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Classificacao>> ObterTodos(bool incluirLivro)
        {
            IQueryable<Classificacao> consulta = _contexto.Classificacao;

            if(incluirLivro)
            {
                consulta = consulta.Include(a => a.Livros);                          
            }

            consulta = consulta.AsNoTracking().OrderBy(a => a.id);

            return await consulta.ToArrayAsync();
        }

        public async Task<IEnumerable<Classificacao>> ObterTodosPeloLivroId(int livroId, bool incluirLivro)
        {
            IQueryable<Classificacao> consulta = _contexto.Classificacao;

            if(incluirLivro)
            {
                consulta = consulta.Include(a => a.Livros);
            }

            consulta = consulta.AsNoTracking()
                                .OrderBy(a => a.id)
                                .Where( a => a.Livros.Any(
                                        ad => ad.id == livroId
                                    )
                                );

            return await consulta.ToArrayAsync();                    
        }

        public async Task<Classificacao> ObterPelaClassificacaoId(int classificacaoId, bool incluirLivro)
        {
            IQueryable<Classificacao> consulta = _contexto.Classificacao;

            if(incluirLivro){
                consulta = consulta.Include(a => a.Livros);                      
            }

            consulta = consulta.AsNoTracking()
                               .OrderBy(a => a.id)
                               .Where(a => a.id == classificacaoId);

            return await consulta.FirstOrDefaultAsync();                   
        }
    }
}