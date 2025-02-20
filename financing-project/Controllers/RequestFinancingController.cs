using financing_project.Interfaces;
using financing_project.Models;
using Microsoft.AspNetCore.Mvc;

namespace financing_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestFinancingController : ControllerBase
    {
        private readonly IRequestFinancingService _requestFinancingService;

        public RequestFinancingController(IRequestFinancingService requestFinancingService)
        {
            _requestFinancingService = requestFinancingService;
        }

        [HttpPost]
        public async Task<ResponseModel<RequestFinancing>> RequestFinancing(RequestFinancing requestFinancing)
        {
            return await _requestFinancingService.FinancingRequested(requestFinancing);
        }
    }
}
