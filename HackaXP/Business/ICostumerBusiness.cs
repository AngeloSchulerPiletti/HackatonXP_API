﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Business.Implementation
{
    public interface ICostumerBusiness
    {
        bool CostumerAllowTest(long costumerId);
        bool CostumerAllowTest(string costumerName);
    }
}
