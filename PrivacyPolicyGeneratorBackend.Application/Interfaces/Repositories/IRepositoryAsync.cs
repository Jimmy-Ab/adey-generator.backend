using PrivacyPolicyGeneratorBackend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PrivacyPolicyGeneratorBackend.Application.Interfaces.Repositories
{
    public interface IRepositoryAsync<T, in TId> where T : IEntity<TId>, IAggregateRoot
    {
        IQueryable<T> Entities { get; }
        Task<T> GetByIdAsync(TId id);
        Task<T> GetSingleWithSpecificationAsync(Expression<Func<T, bool>> expression, List<string> includes = null);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetManyWithSpecificationAsync(
           Expression<Func<T, bool>> expression = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           List<string> includes = null
           );
        Task<List<T>> GetPagedResponseAsync(int pageNumber, int pageSize);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

    }
}
