using System.Threading;
using System.Threading.Tasks;

namespace Exemplo.Application.UseCases.GetCustomers.Abstractions
{
    public interface IGetCustomersUseCase
    {
        Task ExecuteAsync(int customerId, CancellationToken cancellationToken);
        void SetOutputPort(IOutputPort outputPort);

    }
}