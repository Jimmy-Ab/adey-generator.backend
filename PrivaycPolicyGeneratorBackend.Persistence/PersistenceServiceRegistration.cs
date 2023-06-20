using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrivacyPolicyGeneratorBackend.Application.Events;
using PrivacyPolicyGeneratorBackend.Application.Interfaces.Repositories;
using PrivacyPolicyGeneratorBackend.Domain.Events;
using PrivaycPolicyGeneratorBackend.Persistence.Contexts;
using PrivaycPolicyGeneratorBackend.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivaycPolicyGeneratorBackend.Persistence
{
    public static class PersistenceExtension
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("CervisWTMSPostgreSqlConn")));
            services
                .AddTransient(typeof(IRepositoryAsync<,>), typeof(BaseRepository<,>))
                .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
            .AddTransient<IDomainEventDispatcher, MediatrDomainEventDispatcher>();

            return services;
        }
    }
}
