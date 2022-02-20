using HackaXP.Data.DTO;
using HackaXP.Data.DTO.Febraban;
using HackaXP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Repository
{
    public interface ICostumerRepository
    {
        ActionsMessageResult AddCostumer(NewCostumer newCostumer);
        bool CheckIfCostumerExists(long costumerId);
        bool CheckIfCostumerExists(string costumerName);
        Costumer GetCostumerData(long costumerId);
        Costumer GetCostumerData(string costumerName);
        FinancialHealthyHistory GetLastFinancialHealthyConsult(int userId);
        FinancialHealthyHistory GetLastFinancialHealthyConsult(string userName);
        FinancialHealthyHistory SaveFinancialHealthyConsult(FebrabanCompleteResultData febrabanResponse, long costumerId);
        ActionsMessageResult UpdateCostumerAcceptance(CostumerAcceptance acceptanceDto);
    }
}
