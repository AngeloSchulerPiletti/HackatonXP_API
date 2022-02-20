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
        Task<CostumerOpenFinanceData> GetCostumer(string costumerName);
        FebrabanFormVO CalculateFinancialHealthy(CostumerOpenFinanceData costumerData);
        Task<FebrabanResponseData> SendQuestionaryToFebraban(FebrabanFormVO febrabanFormVO);
        Task<FebrabanCompleteResultData> GetFormResultFromFebraban(FebrabanResponseData febrabanResponseData);
    }
}
