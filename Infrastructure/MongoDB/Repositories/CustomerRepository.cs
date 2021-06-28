using Exemplo.Infrastructure.MongoDB.Collections;
using Exemplo.Infrastructure.MongoDB.Configurations;
using Exemplo.Infrastructure.MongoDB.Repositories.Abstractions;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Exemplo.Infrastructure.MongoDB.Repositories
{
    internal sealed class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<CustomerCollection> _customerCollection;
        private const string CustomerCollection = "CustomerCollection";

        public CustomerRepository(IMongoClient mongoClient, MongoDBConfiguration mongoDBConfiguration)
        {
            var mongoDBDatabase = mongoClient.GetDatabase(mongoDBConfiguration.DatabaseName);
            var indexKey = Builders<CustomerCollection>.IndexKeys;
            var constraints = indexKey.Combine(indexKey.Ascending(field => field.CustomerId)); // criando index com o cpf
            _customerCollection = mongoDBDatabase.GetCollection<CustomerCollection>(CustomerCollection);
            _customerCollection.Indexes.CreateOne(new CreateIndexModel<CustomerCollection>(constraints, new CreateIndexOptions { Unique = true }));
        }

        //métodos do crud
        public async Task CreateCustomerAsync(CustomerCollection customerCollection, CancellationToken cancellationToken)
        {
            await _customerCollection.InsertOneAsync(customerCollection, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task UpdateCustomerAsync(int customerId, CustomerCollection customerCollection, CancellationToken cancellationToken)
        {
            var filter = Builders<CustomerCollection>.Filter.Where(select => select.CustomerId == customerId);
            //verificar como atualizar campo a campo
            var result = await _customerCollection.ReplaceOneAsync(filter, customerCollection, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<CustomerCollection> FindByCustomerId(int customerId, CancellationToken cancellationToken)
        {
            var filter = Builders<CustomerCollection>.Filter.Eq(select => select.CustomerId, customerId);
            var result = await _customerCollection.FindAsync(filter, cancellationToken: cancellationToken).ConfigureAwait(false);
            return await result.FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<CustomerCollection>> FindAll()
        {
            var result = await _customerCollection.Find(_ => true).ToListAsync().ConfigureAwait(false);
            return result;
        }

        public async Task RemoveByCustomerId(int customerId, CancellationToken cancellationToken)
        {
            var filter = Builders<CustomerCollection>.Filter.Eq(select => select.CustomerId, customerId);
            await _customerCollection.DeleteOneAsync(filter, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}