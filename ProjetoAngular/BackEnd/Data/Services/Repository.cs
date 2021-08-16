using System.Threading.Tasks;
using BackEnd.Data.Repository.Interfaces;

namespace BackEnd.Data.Services
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;
        public Repository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity)where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity)where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
    }
}