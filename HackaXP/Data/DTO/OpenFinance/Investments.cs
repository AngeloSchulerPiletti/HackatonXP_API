using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Data.DTO.OpenFinance
{
    public class Investments
    {
        public Investments()
        {
            Stocks = new List<Stock>();
            Cdb = new List<CdbAsset>();
            InvestimentFunds = new List<InvestmentFund>();
            SavingsAccount = new List<object>();
            PrivatePension = new List<object>();
            Lci = new List<FixedIncomeAsset>();
            Lca = new List<FixedIncomeAsset>();
            Cri = new List<FixedIncomeAsset>();
            Cra = new List<FixedIncomeAsset>();
        }

        public List<Stock> Stocks { get; set; }
        public List<CdbAsset> Cdb { get; set; }
        public List<InvestmentFund> InvestimentFunds { get; set; }
        public List<object> SavingsAccount { get; set; }
        public List<object> PrivatePension { get; set; }
        public List<FixedIncomeAsset> Lci { get; set; }
        public List<FixedIncomeAsset> Lca { get; set; }
        public List<FixedIncomeAsset> Cri { get; set; }
        public List<FixedIncomeAsset> Cra { get; set; }

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
        public class InvestmentFund
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
