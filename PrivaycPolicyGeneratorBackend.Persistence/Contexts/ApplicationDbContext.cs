using Microsoft.EntityFrameworkCore;
using PrivacyPolicyGeneratorBackend.Domain.Events;
using PrivacyPolicyGeneratorBackend.Domain.Interfaces;
using PrivacyPolicyGeneratorBackend.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivaycPolicyGeneratorBackend.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDomainEventDispatcher _dispatcher;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDomainEventDispatcher dispatcher) : base(options)
        {
            _dispatcher = dispatcher;
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// On Model Creating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity<Guid>>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = DateTime.UtcNow;
                        break;
                }
            }
            await _preSaveChanges();
            return await base.SaveChangesAsync(cancellationToken);
        }
        private async Task _preSaveChanges()
        {
            await _dispatchDomainEvents();
        }
        private async Task _dispatchDomainEvents()
        {
            var domainEventEntities = this.ChangeTracker.Entries<AuditableEntity<Guid>>()
               .Select(po => po.Entity)
               .Where(po => po.DomainEvents.Any())
               .ToArray();

            foreach (var entity in domainEventEntities)
            {
                IDomainEvent dev;
                while (entity.DomainEvents.TryTake(out dev))
                    await _dispatcher.Dispatch(dev);
            }
        }
    }
}
