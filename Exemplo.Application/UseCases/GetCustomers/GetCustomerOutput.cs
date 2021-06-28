using System;
using System.Collections.Generic;
using System.Text;

namespace Exemplo.Application.UseCases.GetCustomers
{
    public sealed class GetCustomerOutput
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public int Age { get; set; }
    }
}
