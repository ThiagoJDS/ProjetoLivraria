using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data.Repository.Interfaces;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data.Services
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly DataContext _contexto;

        public GeneroRepository(DataContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Genero>> ObterTodos(bool incluirLivro)
        {
            IQueryable<Genero> consulta = _contexto.Genero;

            if(incluirLivro)
            {
                consulta = consulta.Include(a => a.Livros);                          
            }

            consulta = consulta.AsNoTracking().OrderBy(a => a.id);

            return await consulta.ToArrayAsync();
        }

        public async Task<IEnumerable<Genero>> ObterTodosPeloLivroId(int livroId, bool incluirLivro)
        {
            IQueryable<Genero> consulta = _contexto.Genero;

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

        public async Task<Genero> ObterPeloGeneroId(int generoId, bool incluirLivro)
        {
            IQueryable<Genero> consulta = _contexto.Genero;

            if(incluirLivro){
                consulta = consulta.Include(a => a.Livros);                      
            }

            consulta = consulta.AsNoTracking()
                               .OrderBy(a => a.id)
                               .Where(a => a.id == generoId);

            return await consulta.FirstOrDefaultAsync();                   
        }
    }
}