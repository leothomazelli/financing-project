using financing_project.Models;

namespace financing_project.Interfaces
{
    public interface ICustomerService
    {
        /// <summary>
        /// This method receives a Customer object, after validation it is then created and persisted into the database.
        /// </summary>
        /// <param name="customer">The customer object received to be validated and inserted in the database.</param>
        /// <returns>It will return a Response Model of Customer, containing the data, message and status of the request.</returns>
        Task<ResponseModel<Customer>> CreateCustomer(Customer customer);

        /// <summary>
        /// This method will search in the database if we already have a customer with that CPF.
        /// </summary>
        /// <param name="cpf">The CPF that'll be searched in the database.</param>
        /// <returns>It will return a Customer object if the CPF exists in the database.</returns>
        Customer GetByCpf(string cpf);

        /// <summary>
        /// This method receives a CPF to format, so that we can maintain a storage standard.
        /// </summary>
        /// <param name="cpf">The CPF that'll be formatted.</param>
        /// <returns>It will return the CPF already formatted.</returns>
        string FormatCpf(string cpf);
    }
}
