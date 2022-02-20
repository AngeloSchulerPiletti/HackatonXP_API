using HackaXP.Data.DTO;
using HackaXP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Repository
{
    public interface ICostumerRepository
    {
        ActionsMessageResult UpdateCostumerAcceptance(CostumerAcceptance acceptanceDto);
        Costumer GetCostumerData(long costumerId);
        Costumer GetCostumerData(string costumerName);
        bool CheckIfCostumerExists(long costumerId);
        bool CheckIfCostumerExists(string costumerName);
        ActionsMessageResult AddCostumer(NewCostumer newCostumer);
    }
}
