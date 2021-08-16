using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data.Repository.Interfaces;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data.Services
{
    public class SegmentoRepository : ISegmentoRepository
    {
        private readonly DataContext _contexto;

        public SegmentoRepository(DataContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Segmento>> ObterTodos(bool incluirLivro)
        {
            IQueryable<Segmento> consulta = _contexto.Segmento;

            if(incluirLivro)
            {
                consulta = consulta.Include(a => a.Livros);                          
            }

            consulta = consulta.AsNoTracking().OrderBy(a => a.id);

            return await consulta.ToArrayAsync();
        }

        public async Task<IEnumerable<Segmento>> ObterTodosPeloLivroId(int livroId, bool incluirLivro)
        {
            IQueryable<Segmento> consulta = _contexto.Segmento;

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

        public async Task<Segmento> ObterPeloSegmentoId(int segmentoId, bool incluirLivro)
        {
            IQueryable<Segmento> consulta = _contexto.Segmento;

            if(incluirLivro){
                consulta = consulta.Include(a => a.Livros);                      
            }

            consulta = consulta.AsNoTracking()
                               .OrderBy(a => a.id)
                               .Where(a => a.id == segmentoId);

            return await consulta.FirstOrDefaultAsync();                   
        }
    }
}