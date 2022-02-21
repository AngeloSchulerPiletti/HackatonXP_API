using HackaXP.Business;
using HackaXP.Business.Implementation;
using HackaXP.Data.DTO;
using HackaXP.Data.DTO.Febraban;
using HackaXP.Data.DTO.OpenFinance;
using HackaXP.Data.VO.Engine;
using HackaXP.Models;
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
            // Verifica quando foi a última vez que o usuario fez a consulta, nao pode ser menos de 7 dias

            CostumerOpenFinanceData costumerData = _openFinanceBusiness.GetCostumer(costumerName).Result;

            FebrabanFormVO febrabanFormVO = _openFinanceBusiness.CalculateFinancialHealthy(costumerData);
            if (febrabanFormVO == null) return BadRequest("Houve um erro ao interpretar as informações do OpenFinance");

            FebrabanResponseData febrabanAnswer = _openFinanceBusiness.SendQuestionaryToFebraban(febrabanFormVO).Result;
            if (!febrabanAnswer.Success) return BadRequest("Houve um erro ao enviar os dados para o serviço da Febraban");

            FebrabanCompleteResultData febrabanFinancialHealthyAnswer = _openFinanceBusiness.GetFormResultFromFebraban(febrabanAnswer).Result;
            if (!febrabanFinancialHealthyAnswer.Success) return BadRequest("Houve um erro ao obter o resultado da sua saúde financeira com o serviço da Febraban");

            Costumer costumer = _costumerRepository.GetCostumerData(costumerName);
            _costumerRepository.SaveFinancialHealthyConsult(febrabanFinancialHealthyAnswer, costumer.Id);

            return Ok(febrabanFinancialHealthyAnswer);
        }

        [HttpGet]
        [Route("user-info/{userName}")]
        public IActionResult GetUserInfo(string userName)
        {
            bool exists =_costumerRepository.CheckIfCostumerExists(userName);
            if (!exists) return BadRequest("Esse usuário não existe");

            Costumer costumer = _costumerRepository.GetCostumerData(userName);
            return Ok(costumer);
        }

        [HttpGet]
        [Route("user-info/{userName}/last-financial-healthy-consult")]
        public IActionResult GetUserFinancialHealthyConsult(string userName)
        {
            bool exists = _costumerRepository.CheckIfCostumerExists(userName);
            if (!exists) return BadRequest("Esse usuário não existe");

            Costumer costumer = _costumerRepository.GetCostumerData(userName);
            if (costumer.LastFinancialHealthyHistoryId == 0) return BadRequest("Você ainda não realizou uma consulta de saúde financeira");

            FinancialHealthyHistory consult = _costumerRepository.GetLastFinancialHealthyConsult(costumer.LastFinancialHealthyHistoryId);

            return Ok(consult);
        }

        [HttpPost]
        [Route("approve-open-finance")]
        public IActionResult ApproveOpenFinance([FromBody] NewCostumer newCostumer)
        {
            if (_costumerRepository.CheckIfCostumerExists(newCostumer.Name)) return BadRequest(new ActionsMessageResult("OpenFinance já está habilitado para este usuário"));
            if (_openFinanceBusiness.GetCostumer(newCostumer.Name).Result == null) return BadRequest(new ActionsMessageResult("Este usuário não existe"));

            ActionsMessageResult result = _costumerRepository.AddCostumer(newCostumer);
            return result.IsError ? BadRequest(result) : Ok(result);
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
