using PrivacyPolicyGeneratorBackend.Domain.Interfaces;
using PrivacyPolicyGeneratorBackend.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivacyPolicyGeneratorBackend.Application.Interfaces.Repositories
{
    public interface IUnitOfWork<TId> : IDisposable
    {
        IRepositoryAsync<T, TId> Repository<T>() where T : AuditableEntity<TId>, IAggregateRoot;
        Task<int> Commit(CancellationToken cancellationToken);
        Task Rollback();
    }
}
