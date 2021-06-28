using Exemplo.Infrastructure.MongoDB.Collections;
using System.Collections.Generic;

namespace Exemplo.Application.UseCases.GetCustomers
{
    internal static class GetCustomersMappers
    {
        internal static GetCustomerOutput MapToOutputCustomerId(this CustomerCollection customerCollection)
        {
            return new GetCustomerOutput
            {
                Age = customerCollection.Age,
                Cpf = customerCollection.Cpf,
                Name = customerCollection.Name,
                CustomerId = customerCollection.CustomerId
            };
        }

        internal static GetAllCustomersOutput MapToOutputCustomers(this IEnumerable<CustomerCollection> customerCollection)
        {

            var response = new GetAllCustomersOutput();

            if (customerCollection != null)
            {
                foreach (var customer in customerCollection)
                {
                    var customerResult = new GetCustomerOutput
                    {
                        Age = customer.Age,
                        Name = customer.Name,
                        Cpf = customer.Cpf,
                        CustomerId = customer.CustomerId
                    };
                    response.Customers.Add(customerResult);
                }         
            }
            return response;
        }





    }
}