using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data.Repository.Interfaces;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data.Services
{
    public class LivroRepository : ILivroRepository
    {
        private readonly DataContext _contexto;

        public LivroRepository(DataContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Livro>> ObterTodos(bool incluirSegmento, 
                                                         bool incluirMarca,
                                                         bool incluirAutor,
                                                         bool incluirClassificacao,
                                                         bool incluirGenero)
        {
            IQueryable<Livro> consulta = _contexto.Livro;

            if(incluirSegmento)
            {
                consulta = consulta.Include(a => a.segmento);                          
            }
            if(incluirMarca)
            {
                consulta = consulta.Include(a => a.marca);                          
            }
            if(incluirAutor)
            {
                consulta = consulta.Include(a => a.autor);                          
            }
            if(incluirClassificacao)
            {
                consulta = consulta.Include(a => a.classificacao);                          
            }
            if(incluirGenero)
            {
                consulta = consulta.Include(a => a.genero);                          
            }

            consulta = consulta.AsNoTracking().OrderBy(a => a.id);

            return await consulta.ToArrayAsync();
        }

        public async Task<Livro> ObterPeloLivroId(int livroId,bool incluirSegmento,
                                                              bool incluirMarca,
                                                              bool incluirAutor,
                                                              bool incluirClassificacao,
                                                              bool incluirGenero)
        {
            IQueryable<Livro> consulta = _contexto.Livro;

            if(incluirSegmento){
                consulta = consulta.Include(a => a.segmento);
            }
            if(incluirMarca)
            {
                consulta = consulta.Include(a => a.marca);
            }
            if(incluirAutor)
            {
                consulta = consulta.Include(a => a.autor);
            }
            if(incluirClassificacao)
            {
                consulta = consulta.Include(a => a.classificacao);
            }
            if(incluirGenero)
            {
                consulta = consulta.Include(a => a.genero);
            }

            consulta = consulta.AsNoTracking()
                               .OrderBy(a => a.id)
                               .Where(a => a.id == livroId);

            return await consulta.FirstOrDefaultAsync();
        }
    }
}