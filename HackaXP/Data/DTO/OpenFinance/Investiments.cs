using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Data.DTO.OpenFinance
{
    public class Investiments
    {
        public Stock[] Stocks { get; set; }
        public CdbAsset[] Cdb { get; set; }
        public investimentFund[] InvestimentFunds { get; set; }
        public object[] SavingsAccount { get; set; }
        public object[] PrivatePension { get; set; }
        public FixedIncomeAsset[] Lci { get; set; }
        public FixedIncomeAsset[] Lca { get; set; }
        public FixedIncomeAsset[] Cri { get; set; }
        public FixedIncomeAsset[] Cra { get; set; }

        public class Stock
        {
            public string Identity { get; set; }
            public string BankId { get; set; }
            public string Ticker { get; set; }
            public int Volume { get; set; }
            public float Value { get; set; }
            public DateTime AcquisitionDate { get; set; }
            public int Risk { get; set; }
        }
        public class CdbAsset
        {
            public string Identity { get; set; }
            public string BankId { get; set; }
            public string Description { get; set; }
            public string Type { get; set; }
            public float Value { get; set; }
            public DateTime DueDate { get; set; }
            public int Profitability { get; set; }
            public int Risk { get; set; }
            public DateTime AcquisitionDate { get; set; }
            public int Volume { get; set; }
        }
        public class investimentFund
        {
            public string Identity { get; set; }
            public string BankId { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public float Value { get; set; }
            public DateTime AcquisitionDate { get; set; }
            public int Risk { get; set; }
            public int Volume { get; set; }
        }
        //public class SavingFund
        //{

        //}
        //public class PrivatePension
        //{

        //}
        public class FixedIncomeAsset
        {
            public string Identity { get; set; }
            public string BankId { get; set; }
            public string Description { get; set; }
            public string Type { get; set; }
            public float Value { get; set; }
            public DateTime DueDate { get; set; }
            public int Profitability { get; set; }
            public int Risk { get; set; }
            public DateTime AcquisitionDate { get; set; }
            public int Volume { get; set; }

        }


    }
}
