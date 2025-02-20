using financing_project.Data;
using financing_project.Enums;
using financing_project.Interfaces;
using financing_project.Models;

namespace financing_project.Services
{
    public class FinancingService : IFinancingService
    {
        #region Properties

        /// <summary>
        /// AppDbContext object responsible to stablish the connection with database using EntityFramework.
        /// </summary>
        private readonly AppDbContext _context;

        /// <summary>
        /// ICustomerService object responsible to provide access to customer service methods.
        /// </summary>
        private readonly ICustomerService _customerService;

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Constructor used to initialize AppDbContext and ICustomerService using dependency injection.
        /// </summary>
        /// <param name="context">AppDbContext object used to initialize internal variable using dependency injection.</param>
        /// <param name="customerService">ICustomerService object used to initialize internal variable using dependency injection.</param>
        public FinancingService(AppDbContext context, ICustomerService customerService)
        {
            _context = context;
            _customerService = customerService;
        }

        /// <summary>
        /// This method receives a Financing object, after validation it is then created and persisted into the database.
        /// </summary>
        /// <param name="newFinancing">The financing object received to be validated and inserted in the database.</param>
        /// <returns>It will return a Response Model of Financing, containing the data, message and status of the request.</returns>
        public async Task<ResponseModel<Financing>> CreateFinancing(Financing newFinancing)
        {
            ResponseModel<Financing> response = new ResponseModel<Financing>();
            try
            {
                if (newFinancing == null)
                {
                    throw new Exception("Informe os dados.");
                }
                if (newFinancing.Total > 1000000000)
                {
                    throw new Exception("O valor máximo de Financiamento é de R$1.000.000.000,00 (Um milhão).");
                }
                if (newFinancing.Type == Enums.FinancingTypeEnum.PJ)
                {
                    if (newFinancing.Total < 15000)
                    {
                        throw new Exception("Financiamentos do tipo Pessoa Jurídica não podem ser inferiores à R$15.000,00 (Quinze mil).");
                    }
                }
                newFinancing.Cpf = _customerService.FormatCpf(newFinancing.Cpf);
                _context.Add(newFinancing);
                await _context.SaveChangesAsync();

                response.Message = "Financiamento cadastrado com sucesso.";
                response.Data = newFinancing; 
            } catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
            return response;
        }

        /// <summary>
        /// This method will search in the database if we already have a financing with that id.
        /// </summary>
        /// <param name="id">The id that'll be searched in the database.</param>
        /// <returns>It will return a Financing object if the id exists in the database.</returns>
        public Financing GetFinancingById(int id)
        {
            return _context.Financing.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// This method receives a Financing object, then it'll apply interest depending on the type of financing the customer chose. 
        /// </summary>
        /// <param name="data">The financing object that'll be applied interest.</param>
        /// <returns>It returns the financing value with interest applied.</returns
        public float ApplyFinancingTypeInterests(Financing data)
        {
            if (data.Type == Enums.FinancingTypeEnum.Direto)
            {
                return data.Total += 2f / 100f * data.Total;
            }

            if (data.Type == Enums.FinancingTypeEnum.Consignado)
            {
                return data.Total += 1f / 100f * data.Total;
            }

            if (data.Type == Enums.FinancingTypeEnum.PJ)
            {
                return data.Total += 5f / 100f * data.Total; ;
            }

            if (data.Type == Enums.FinancingTypeEnum.PF)
            {
                return data.Total += 3f / 100f * data.Total; ;
            }

            if (data.Type == Enums.FinancingTypeEnum.Imobiliário)
            {
                return data.Total += 9f / 100f * data.Total;
            }
            return data.Total;
        }

        #endregion Public methods
    }
}
