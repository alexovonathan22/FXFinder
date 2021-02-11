using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FXFinder.Core.DBModels;

namespace FXFinder.Core.DataAccess
{
    public interface IRepository<T> where T : BaseEntity
    {

        Task<T> LoadById(int id);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task<T> Insert(T entity);
        Task Update(T entity);
        Task UpdateLists(List<T> entities);
        Task Remove(T entity);
        Task<List<T>> LoadAll();
        Task<List<T>> LoadWhere(Expression<Func<T, bool>> predicate);
        Task<int> CountAll();
        Task<int> CountWhere(Expression<Func<T, bool>> predicate);
    }
}
