using financing_project.Interfaces;
using financing_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace financing_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<ResponseModel<Customer>> CreateCustomer(Customer Customer)
        {
            return await _customerService.CreateCustomer(Customer);
        }
    }
}
