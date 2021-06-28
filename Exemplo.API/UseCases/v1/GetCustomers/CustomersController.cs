using Exemplo.Application.UseCases.GetCustomers;
using Exemplo.Application.UseCases.GetCustomers.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Exemplo.API.UseCases.v1.GetCustomers
{
    [Route("api/v1/customers")]
    [ApiController]
    public class CustomersController : ControllerBase, IOutputPort
    {
        private readonly IGetCustomersUseCase _useCase;
        [NotNull] private ActionResult? _viewModel;

        public CustomersController(IGetCustomersUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpGet("{customerId}", Name = nameof(CustomersController))]
        public async Task<ActionResult> GetById(int customerId, CancellationToken cancellationToken)
        {
            _useCase.SetOutputPort(this);
            await _useCase.ExecuteAsync(customerId, cancellationToken).ConfigureAwait(false);
            return _viewModel;
        }

        public void Invalid(string warning) => _viewModel = StatusCode(StatusCodes.Status500InternalServerError,
            CustomerResponse.Error(warning));

        public void NotAccepted() => _viewModel = StatusCode(StatusCodes.Status400BadRequest,
            CustomerResponse.Error("Invalid parameters"));

        public void Success(GetCustomerOutput customer) => _viewModel = StatusCode(StatusCodes.Status200OK,
            CustomerResponse.Success(customer));

        public void SuccessAll(GetAllCustomersOutput allCustomers)
        {
            //Implementar o retorno da lista no base response
            throw new NotImplementedException();
        }
    }
}