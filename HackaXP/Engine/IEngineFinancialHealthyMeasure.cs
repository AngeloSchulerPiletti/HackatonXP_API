using HackaXP.Data.DTO.OpenFinance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Data.VO.Engine
{
    public interface IEngineFinancialHealthyMeasure
    {
        EngineOwnMeasureVO Calculate(CostumerOpenFinanceData costumerData);
        FebrabanFormVO TranslateToFebrabanJson(EngineOwnMeasureVO engineOwnMeasureVO);
    }
}
