using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data.Repository.Interfaces;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data.Services
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly DataContext _contexto;

        public MarcaRepository(DataContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Marca>> ObterTodos(bool incluirLivro)
        {
            IQueryable<Marca> consulta = _contexto.Marca;

            if(incluirLivro)
            {
                consulta = consulta.Include(a => a.Livros);                          
            }

            consulta = consulta.AsNoTracking().OrderBy(a => a.id);

            return await consulta.ToArrayAsync();
        }

        public async Task<IEnumerable<Marca>> ObterTodosPeloLivroId(int livroId, bool incluirLivro)
        {
            IQueryable<Marca> consulta = _contexto.Marca;

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

        public async Task<Marca> ObterPelaMarcaId(int marcaId, bool incluirLivro)
        {
            IQueryable<Marca> consulta = _contexto.Marca;

            if(incluirLivro){
                consulta = consulta.Include(a => a.Livros);                      
            }

            consulta = consulta.AsNoTracking()
                               .OrderBy(a => a.id)
                               .Where(a => a.id == marcaId);

            return await consulta.FirstOrDefaultAsync();                   
        }
    }
}