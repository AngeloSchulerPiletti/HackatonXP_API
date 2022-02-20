using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Data.DTO.OpenFinance
{
    public class CostumerOpenFinanceData
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public float Salary { get; set; }
        public DateTime BornDate { get; set; }
        public Bank[] Banks { get; set; }
    }
}
