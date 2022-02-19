using HackaXP.Business.Implementation;
using HackaXP.Data.DTO;
using HackaXP.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CostumerController : ControllerBase
    {
        private ICostumerBusiness _costumerBusiness;
        private ICostumerRepository _costumerRepository;
        public CostumerController (ICostumerBusiness costumerBusiness)
        {
            _costumerBusiness = costumerBusiness;
        }

        [HttpPatch]
        [Route("acceptance-financial-healthy-consult")]
        public IActionResult AcceptanceFinancialHealthyConsult([FromBody] CostumerAcceptance acceptanceDto)
        {
            ActionsMessageResult result = _costumerRepository.UpdateCostumerAcceptance(acceptanceDto);
            return result.IsError ? BadRequest(result) : Ok(result);
        }
    }
}
