using HackaXP.Business;
using HackaXP.Business.Implementation;
using HackaXP.Data.DTO;
using HackaXP.Data.DTO.OpenFinance;
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
        private IOpenFinanceBusiness _openFinanceBusiness;
        public CostumerController(ICostumerBusiness costumerBusiness, ICostumerRepository costumerRepository, IOpenFinanceBusiness openFinanceBusiness)
        {
            _costumerBusiness = costumerBusiness;
            _costumerRepository = costumerRepository;
            _openFinanceBusiness = openFinanceBusiness;
        }

        [HttpPost]
        [Route("calculate-financial-healthy")]
        public IActionResult CalculateFinancialHealthy([FromBody] string costumerName)
        {
            if (!_costumerRepository.CheckIfCostumerExists(costumerName)) return BadRequest(new ActionsMessageResult("Cliente não existe ou não aprovou OpenFinance"));
            if (!_costumerBusiness.CostumerAllowTest(costumerName)) return BadRequest(new ActionsMessageResult("Este cliente ainda não aprovou o OpenFinance"));

            CostumerOpenFinanceData costumerData = _openFinanceBusiness.GetCostumer(costumerName).Result;

            var answer = _openFinanceBusiness.CalculateFinancialHealthy(costumerData);

            return Ok(answer);
        }

        [HttpPost]
        [Route("approve-open-finance")]
        public IActionResult ApproveOpenFinance([FromBody] NewCostumer newCostumer)
        {
            if (_costumerRepository.CheckIfCostumerExists(newCostumer.Name)) return BadRequest(new ActionsMessageResult("OpenFinance já está habilitado para este usuário"));
            //Verifica se o usuário existe na api da xp

            ActionsMessageResult result = _costumerRepository.AddCostumer(newCostumer);
            //return result.IsError ? BadRequest(result) : Ok(result);
            return Ok();
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
