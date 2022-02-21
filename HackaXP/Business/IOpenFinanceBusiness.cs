using HackaXP.Data.DTO.Febraban;
using HackaXP.Data.DTO.OpenFinance;
using HackaXP.Data.VO.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Business
{
    public interface IOpenFinanceBusiness
    {
        FebrabanFormVO CalculateFinancialHealthy(CostumerOpenFinanceData costumerData);
        //Task<bool> CheckIfUserExistsInOpenFinanceXp(string costumerName);
        Task<CostumerOpenFinanceData> GetCostumer(string costumerName);
        Task<FebrabanCompleteResultData> GetFormResultFromFebraban(FebrabanResponseData febrabanResponseData);
        Task<FebrabanResponseData> SendQuestionaryToFebraban(FebrabanFormVO febrabanFormVO);
    }
}
