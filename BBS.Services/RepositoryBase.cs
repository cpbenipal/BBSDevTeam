using BBS.Entities;
using BBS.Services.Contracts;
using Microsoft.EntityFrameworkCore; 

namespace BBS.Services.Repository
{
        public class RepositoryBase<T> : IGenericRepository<T> where T : class
    {
        private readonly BusraDbContext _context;
        private readonly DbSet<T> table;
        public RepositoryBase()
        {
            this._context = new BusraDbContext();
            table = _context.Set<T>();
        }
        public RepositoryBase(BusraDbContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return table.AsNoTracking().ToList();
        }
        public T GetById(object id)
        {
            return table.Find(id)!;
        }
        public T Insert(T obj)
        {
            table.Add(obj);
            return obj;
        }
        public IEnumerable<T> InsertRange(IEnumerable<T> obj)
        {
            table.AddRange(obj);
            return obj;
        }
        public T Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            return obj;
        }
        public IEnumerable<T> UpdateRange(IEnumerable<T> obj)
        {
            table.AttachRange(obj);
            _context.Entry(obj).State = EntityState.Modified;
            return obj;
        }
        public void Delete(object id)
        {
            T existing = table.Find(id)!;
            table.Remove(existing);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}