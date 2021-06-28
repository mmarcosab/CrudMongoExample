using Exemplo.Application.UseCases.GetCustomers.Abstractions;
using Exemplo.Infrastructure.MongoDB.Repositories.Abstractions;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Exemplo.Application.UseCases.GetCustomers
{
    public sealed class GetCustomersUseCase : IGetCustomersUseCase
    {
        private readonly ICustomerRepository _repository;

        [NotNull] private IOutputPort _outputPort;

        public GetCustomersUseCase(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(int customerId, CancellationToken cancellationToken)
        {
            if (customerId != 0)
            { 
                var customer = await _repository.FindByCustomerId(customerId, cancellationToken: cancellationToken).ConfigureAwait(false);
                var response = GetCustomersMappers.MapToOutputCustomerId(customer);
                if (response == null) 
                {
                    _outputPort!.Invalid("Não foi possivel retornar nenhum cliente");
                }
                _outputPort!.Success(response);
                return;
            }
            else
            {
                var customers = await _repository.FindAll().ConfigureAwait(false);
                var response = GetCustomersMappers.MapToOutputCustomers(customers);
                _outputPort!.SuccessAll(response);
                return;
            }
        }

        public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;
    }
}