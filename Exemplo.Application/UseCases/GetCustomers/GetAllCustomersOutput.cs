using System.Collections.Generic;


namespace Exemplo.Application.UseCases.GetCustomers
{
    public sealed class GetAllCustomersOutput
    {
        public IList<GetCustomerOutput> Customers { get; set; }
    }
}