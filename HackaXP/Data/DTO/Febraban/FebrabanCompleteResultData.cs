using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Data.DTO.Febraban
{
    public class FebrabanCompleteResultData
    {
        public bool Success { get; set; }
        public DataIn Data { get; set; }

        public class DataIn
        {
            public SummaryIn Summary { get; set; }
            public dynamic Detailed { get; set; }
            public class SummaryIn
            {
                public int IndexScore { get; set; }
                public int FinancialSecurityScore { get; set; }
                public int FinancialKnowledgeScore { get; set; }
                public int FinancialBehaviorScore { get; set; }
                public int FinancialFreedomScore { get; set; }
                public int BaseScore { get; set; }
            }
        }
    }

}
