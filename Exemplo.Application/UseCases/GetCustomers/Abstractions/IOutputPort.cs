namespace Exemplo.Application.UseCases.GetCustomers.Abstractions
{
    //vai gerenciar a aplicação
    public interface IOutputPort
    {
        void Invalid(string warning);
        void NotAccepted();
        void Success(GetCustomerOutput customer);
        void SuccessAll(GetAllCustomersOutput allCustomers);
    }
}