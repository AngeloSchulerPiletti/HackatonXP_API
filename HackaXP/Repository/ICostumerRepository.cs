using HackaXP.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Repository
{
    public interface ICostumerRepository
    {
        ActionsMessageResult UpdateCostumerAcceptance(CostumerAcceptance acceptanceDto);
    }
}
