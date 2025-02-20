using financing_project.Interfaces;
using financing_project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace financing_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstallmentController : ControllerBase
    {
        private readonly IInstallmentService _installmentService;

        public InstallmentController(IInstallmentService installmentService)
        {
            _installmentService = installmentService;
        }

        [HttpPost]
        public async Task<ResponseModel<Installment>> CreateInstallment(Installment Installment)
        {
            return await _installmentService.CreateInstallment(Installment);
        }
    }
}
