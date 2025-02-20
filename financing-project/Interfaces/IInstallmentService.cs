using financing_project.Models;

namespace financing_project.Interfaces
{
    public interface IInstallmentService
    {
        /// <summary>
        /// This method receives a Installment object, after validation it is then created and persisted into the database.
        /// </summary>
        /// <param name="installment">The installment object received to be validated and inserted in the database.</param>
        /// <returns>It will return a Response Model of Installment, containing the data, message and status of the request.</returns>
        Task<ResponseModel<Installment>> CreateInstallment(Installment installment);
    }
}
