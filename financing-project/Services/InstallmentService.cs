using financing_project.Data;
using financing_project.Interfaces;
using financing_project.Models;

namespace financing_project.Services
{
    public class InstallmentService : IInstallmentService
    {
        #region Properties

        /// <summary>
        /// AppDbContext object responsible to stablish the connection with database using EntityFramework.
        /// </summary>
        private readonly AppDbContext _context;

        /// <summary>
        /// IFinancingService object responsible to provide access to financing service methods.
        /// </summary>
        private readonly IFinancingService _financingService;

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Constructor used to initialize AppDbContext and IFinancingService using dependency injection.
        /// </summary>
        /// <param name="context">AppDbContext object used to initialize internal variable using dependency injection.</param>
        /// <param name="financingService">IFinancingService object used to initialize internal variable using dependency injection.</param>
        public InstallmentService(AppDbContext context, IFinancingService financingService)
        {
            _context = context;
            _financingService = financingService;
        }

        /// <summary>
        /// This method receives a Installment object, after validation it is then created and persisted into the database.
        /// </summary>
        /// <param name="newInstallment">The installment object received to be validated and inserted in the database.</param>
        /// <returns>It will return a Response Model of Installment, containing the data, message and status of the request.</returns>
        public async Task<ResponseModel<Installment>> CreateInstallment(Installment newInstallment)
        {
            ResponseModel<Installment> response = new ResponseModel<Installment>();
            var financing = _financingService.GetFinancingById(newInstallment.FinancingId);
            try
            {
                if (newInstallment == null)
                {
                    throw new Exception("Favor informar os dados.");
                }

                if (newInstallment.FinancingId != financing.Id)
                {
                    throw new Exception("Financiamento não encontrado.");
                }

                if (newInstallment.ExpirationDate < DateTime.UtcNow.AddDays(15) || newInstallment.ExpirationDate > DateTime.UtcNow.AddDays(40)) 
                {
                    throw new Exception("Vencimento não permitido, por favor, selecione uma data mínima de até 15 dias ou máxima de até 40 dias a partir de hoje.");
                }

                if (newInstallment.InstallmentNumber < 5 || newInstallment.InstallmentNumber > 72)
                {
                    throw new Exception("Quantidade de parcelas não permitida, por favor, selecione entre 5 e 72 parcelas.");
                }
                _context.Add(newInstallment);
                await _context.SaveChangesAsync();

                response.Message = "Parcelas cadastradas com sucesso.";
                response.Data = newInstallment;
            } catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
            return response;
        }

        #endregion Public methods
    }
}
