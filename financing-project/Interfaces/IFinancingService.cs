using financing_project.Models;

namespace financing_project.Interfaces
{
    public interface IFinancingService
    {
        /// <summary>
        /// This method receives a Financing object, after validation it is then created and persisted into the database.
        /// </summary>
        /// <param name="financing">The financing object received to be validated and inserted in the database.</param>
        /// <returns>It will return a Response Model of Financing, containing the data, message and status of the request.</returns>
        Task<ResponseModel<Financing>> CreateFinancing(Financing financing);

        /// <summary>
        /// This method will search in the database if we already have a financing with that id.
        /// </summary>
        /// <param name="id">The id that'll be searched in the database.</param>
        /// <returns>It will return a Financing object if the id exists in the database.</returns>
        Financing GetFinancingById(int id);

        /// <summary>
        /// This method receives a Financing object, then it'll apply interest depending on the type of financing the customer chose. 
        /// </summary>
        /// <param name="data">The financing object that'll be applied interest.</param>
        /// <returns>It returns the financing value with interest applied.</returns>
        float ApplyFinancingTypeInterests(Financing data);
    }
}