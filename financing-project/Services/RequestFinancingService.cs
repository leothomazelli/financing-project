using financing_project.Interfaces;
using financing_project.Models;

namespace financing_project.Services
{
    public class RequestFinancingService : IRequestFinancingService
    {
        #region Properties

        /// <summary>
        /// ICustomerService object responsible to provide access to customer service methods.
        /// </summary>
        private readonly ICustomerService _customerService;

        /// <summary>
        /// IFinancingService object responsible to provide access to financing service methods.
        /// </summary>
        private readonly IFinancingService _financingService;

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Constructor used to initialize ICustomerService and IFinancingService using dependency injection.
        /// </summary>
        /// <param name="customerService">ICustomerService object used to initialize internal variable using dependency injection.</param>
        /// <param name="financingService">IFinancingService object used to initialize internal variable using dependency injection.</param>
        public RequestFinancingService(ICustomerService customerService, IFinancingService financingService)
        {
            _customerService = customerService;
            _financingService = financingService;
        }

        /// <summary>
        /// This method receives a Request Financing object, after validation it is then created and persisted into the respectives tables in the database.
        /// </summary>
        /// <param name="requestFinancing">The request financing object received to be validated and inserted into the respective tables in the database.</param>
        /// <returns>It will return a Response Model of RequestFinancing, containing the data, message and status of the request.</returns>
        public async Task<ResponseModel<RequestFinancing>> FinancingRequested(RequestFinancing requestFinancing)
        {
            ResponseModel<RequestFinancing> response = new ResponseModel<RequestFinancing>();
            try
            {
                if (requestFinancing == null)
                {
                    throw new Exception("Informe os dados.");
                }
                var customer = _customerService.GetByCpf(requestFinancing.Cpf);
                if (customer == null)
                {
                    throw new Exception("Cliente não encontrado.");
                }
                if (requestFinancing.Type == Enums.FinancingTypeEnum.PJ && requestFinancing.Value < 15000)
                {
                    throw new Exception("Financiamentos do tipo Pessoa Jurídica não podem ser inferiores à R$15.000,00 (Quinze mil).");
                }
                var financing = new Financing()
                {
                    Total = requestFinancing.Value,
                    Type = requestFinancing.Type,
                    Cpf = requestFinancing.Cpf,
                    LastExpirationDate = requestFinancing.FirstExpirationDate.AddMonths(requestFinancing.TotalInstallments)
                };
                financing.Total = _financingService.ApplyFinancingTypeInterests(financing);

                for (var i = 0; i < requestFinancing.TotalInstallments; i++)
                {
                    var installment = new Installment()
                    {
                        InstallmentNumber = i + 1,
                        InstallmentValue = financing.Total / requestFinancing.TotalInstallments,
                        ExpirationDate = requestFinancing.FirstExpirationDate.AddMonths(i)
                    };

                    if (financing.Installments == null)
                    {
                        financing.Installments = new List<Installment>();
                    }
                    financing.Installments.Add(installment);
                }
                var responseFinancing = await _financingService.CreateFinancing(financing);
                if (responseFinancing.Status == false) 
                {
                    throw new Exception(responseFinancing.Message);
                }

                requestFinancing.Value = financing.Total;
                response.Data = requestFinancing;
                response.Message = "Financiamento aceito!";
            } catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
            }
            return response;
        }

        #endregion Public methods
    }
}