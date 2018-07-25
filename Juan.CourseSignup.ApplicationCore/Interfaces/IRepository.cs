using Juan.CourseSignup.ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Juan.CourseSignup.ApplicationCore.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(long id);
        IEnumerable<T> ListAll();
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }

    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(long id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
