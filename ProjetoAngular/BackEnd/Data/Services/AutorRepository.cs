using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data.Repository.Interfaces;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data.Services
{
    public class AutorRepository : IAutorRepository
    {
        private readonly DataContext _contexto;

        public AutorRepository(DataContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Autor>> ObterTodos(bool incluirLivro)
        {
            IQueryable<Autor> consulta = _contexto.Autor;

            if(incluirLivro)
            {
                consulta = consulta.Include(a => a.Livros);
            }

            consulta = consulta.AsNoTracking().OrderBy(a => a.id);

            return await consulta.ToArrayAsync();
        }

        public async Task<IEnumerable<Autor>> ObterTodosPeloLivroId(int livroId, bool incluirLivro)
        {
            IQueryable<Autor> consulta = _contexto.Autor;

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

        public async Task<Autor> ObterPeloAutorId(int autorId, bool incluirLivro)
        {
            IQueryable<Autor> consulta = _contexto.Autor;

            if(incluirLivro){
                consulta = consulta.Include(a => a.Livros);
            }

            consulta = consulta.AsNoTracking()
                               .OrderBy(a => a.id)
                               .Where(a => a.id == autorId);

            return await consulta.FirstOrDefaultAsync();
        }

    }
}