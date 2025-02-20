using financing_project.Models;

namespace financing_project.Interfaces
{
    public interface IRequestFinancingService
    {
        /// <summary>
        /// This method receives a Request Financing object, after validation it is then created and persisted into the respectives tables in the database.
        /// </summary>
        /// <param name="requestFinancing">The request financing object received to be validated and inserted into the respective tables in the database.</param>
        /// <returns>It will return a Response Model of RequestFinancing, containing the data, message and status of the request.</returns>
        Task<ResponseModel<RequestFinancing>> FinancingRequested(RequestFinancing requestFinancing);
    }
}