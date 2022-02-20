using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Data.DTO.OpenFinance
{
    public class Transactions
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public float Value { get; set; }
        public DateTime Date { get; set; }
        public bool isEntry
        {
            get
            {
                if (Type == "refund" || Type == "credit") return true;
                return false;
            }
            set { }
        }
    }
}
