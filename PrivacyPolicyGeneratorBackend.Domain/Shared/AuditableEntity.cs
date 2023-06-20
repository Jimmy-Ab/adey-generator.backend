using PrivacyPolicyGeneratorBackend.Domain.Events;
using PrivacyPolicyGeneratorBackend.Domain.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PrivacyPolicyGeneratorBackend.Domain.Shared
{
    public class AuditableEntity<TId> : IAuditableEntity<TId>
    {
        public TId Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        private readonly ConcurrentQueue<IDomainEvent> _domainEvents = new ConcurrentQueue<IDomainEvent>();
        public IProducerConsumerCollection<IDomainEvent> DomainEvents => _domainEvents;

        protected AuditableEntity(TId id)
        {
            if (object.Equals(id, default(TId)))
            {
                throw new ArgumentException("The ID cannot be the typ's defualt value.", "id");
            }
            Id = id;
        }
        public AuditableEntity()
        {

        }
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            this._domainEvents.Enqueue(domainEvent);
        }
        public void SetCreatedBy(string createdBy)
        {
            CreatedBy = createdBy;
        }

        public void SetCreatedOn(DateTime dateTime)
        {
            CreatedOn = dateTime;
        }
        public void SetLastModifiedBy(string lastModifiedBy)
        {
            LastModifiedBy = lastModifiedBy;
        }
        public void SetLastModifiedOn(DateTime dateTime)
        {
            LastModifiedOn = dateTime;
        }
    }
}
