using financing_project.Interfaces;
using financing_project.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace financing_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancingController : ControllerBase
    {
        private readonly IFinancingService _financingService;
        public FinancingController(IFinancingService financingService)
        {
            _financingService = financingService;
        }

        [HttpPost]
        [EndpointDescription("Os tipos disponíveis são: Direto, Consignado, PJ, PF e Imobiliário.")]
        public async Task<ResponseModel<Financing>> CreateFinancing(Financing Financing)
        {
            return await _financingService.CreateFinancing(Financing);
        }
    }
}
