
using Microsoft.EntityFrameworkCore;
using Parking_Zone.Data;

namespace Parking_Zone.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> entities;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }
        public T GetById(Guid id)
        {
            return entities.Find(id);
        }
        public void Insert(T entity)
        {
            entities.Add(entity);
        }
        public void Delete(T entity)
        {
            entities.Remove(entity);
        }

        public void Update(T entity)
        {
            entities.Update(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
