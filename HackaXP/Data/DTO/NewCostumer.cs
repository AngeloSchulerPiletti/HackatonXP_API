using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Data.DTO
{
    public class NewCostumer
    {
        public NewCostumer(string name, bool allowTest = true, bool allowOpenFinance = true)
        {
            Name = name;
            AllowTest = allowTest;
            AllowOpenFinance = allowOpenFinance;
        }

        public string Name { get; set; }

        public bool AllowTest { get; set; }

        public bool AllowOpenFinance { get; set; }


    }
}
