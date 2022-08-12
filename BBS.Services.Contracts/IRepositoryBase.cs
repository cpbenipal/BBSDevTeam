
namespace BBS.Services.Contracts
{  
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        T Insert(T obj);
        IEnumerable<T> InsertRange(IEnumerable<T> obj);
        T Update(T obj);
        IEnumerable<T> UpdateRange(IEnumerable<T> obj);
        void Delete(T existing);
        void RemoveRange(IEnumerable<T> entities);
        void Save();
    }
}
