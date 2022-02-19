using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Data.DTO
{
    public class CostumerAcceptance
    {
        public CostumerAcceptance(bool acceptance, long costumerId)
        {
            Acceptance = acceptance;
            CostumerId = costumerId;
        }

        public bool Acceptance { get; }
        public long CostumerId { get; }
    }
}
