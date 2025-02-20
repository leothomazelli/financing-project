using financing_project.Data;
using financing_project.Interfaces;
using financing_project.Models;

namespace financing_project.Services
{
    public class CustomerService : ICustomerService
    {
        #region Properties

        /// <summary>
        /// AppDbContext object responsible to stablish the connection with database using EntityFramework.
        /// </summary>
        private readonly AppDbContext _context;

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Constructor used to initialize AppDbContext using dependency injection.
        /// </summary>
        /// <param name="context">AppDbContext object used to initialize internal variable using dependency injection.</param>
        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This method receives a Customer object, after validation it is then created and persisted into the database.
        /// </summary>
        /// <param name="newCustomer">The customer object received to be validated and inserted in the database.</param>
        /// <returns>It will return a Response Model of Customer, containing the data, message and status of the request.</returns>
        public async Task<ResponseModel<Customer>> CreateCustomer(Customer newCustomer)
        {
            ResponseModel<Customer> response = new ResponseModel<Customer>();
            try
            {
                if (newCustomer == null)
                {
                    throw new Exception("Informe os dados.");
                }
                if (GetByCpf(newCustomer.Cpf) != null)
                {
                    throw new Exception("Este CPF já possui um cadastro.");
                }
                newCustomer.Cpf = FormatCpf(newCustomer.Cpf);
                _context.Add(newCustomer);
                await _context.SaveChangesAsync();

                response.Message = "Cliente cadastrado com sucesso.";
                response.Data = newCustomer;
            } catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
            return response;
        }

        /// <summary>
        /// This method will search in the database if we already have a customer with that CPF.
        /// </summary>
        /// <param name="cpf">The CPF that'll be searched in the database.</param>
        /// <returns>It will return a Customer object if the CPF exists in the database.</returns>
        public Customer GetByCpf(string cpf)
        {
            return _context.Customer.FirstOrDefault(x => x.Cpf == cpf);
        }

        /// <summary>
        /// This method receives a CPF to format, so that we can maintain a storage standard.
        /// </summary>
        /// <param name="cpf">The CPF that'll be formatted.</param>
        /// <returns>It will return the CPF already formatted.</returns>
        public string FormatCpf(string cpf)
        {
            return cpf = new string(cpf.Where(char.IsDigit).ToArray());
        }

        #endregion Public methods
    }
}
