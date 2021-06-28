using Exemplo.Infrastructure.MongoDB.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Exemplo.Infrastructure.MongoDB.Repositories.Abstractions
{
    public interface ICustomerRepository
    {
        public Task CreateCustomerAsync(CustomerCollection customerCollection, CancellationToken cancellationToken);

        public Task UpdateCustomerAsync(int customerId, CustomerCollection customerCollection, CancellationToken cancellationToken);

        public Task<CustomerCollection> FindByCustomerId(int customerId, CancellationToken cancellationToken);

        public Task<IEnumerable<CustomerCollection>> FindAll();

        public Task RemoveByCustomerId(int customerId, CancellationToken cancellationToken);
    }
}