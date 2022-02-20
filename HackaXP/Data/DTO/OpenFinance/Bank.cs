
using HackaXP.Data.DTO.OpenFinance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Data.DTO.OpenFinance
{
    public class Bank
    {
        public int Suitablity { get; set; }
        public DateTime StartDate { get; set; }
        public Institution Institution { get; set; }
        public CreditCard CreditCard { get; set; }
        public Checking Checking { get; set; }
        public Saving Saving { get; set; }
        public PixTransaction[] PixHistory { get; set; }
        public CreditLine[] ConsumedCreditLines { get; set; }
        public Investiments Investiments { get; set; }
        public Bill[] Bills { get; set; }
    }

    public class Institution
    {
        public string Agency { get; set; }
        public string Number { get; set; }
        public string BankId { get; set; }
        public string BankName { get; set; }
    }

    public class CreditCard
    {
        public float Limit { get; set; }
        public Transactions[] Transactions { get; set; }
    }

    public class Checking
    {
        public float Balance { get; set; }
        public float Limit { get; set; }
        public Transactions[] Transactions { get; set; }

    }

    public class Saving
    {
        public float Balance { get; set; }
    }

    public class PixTransaction
    {
        public AccountData From { get; set; }
        public AccountData To { get; set; }
        public string Description { get; set; }
        public float Value { get; set; }
        public DateTime Date { get; set; }

        public class AccountData
        {
            public string BankName { get; set; }
            public string Agency { get; set; }
            public int AccountNumber { get; set; }
            public string Cpf { get; set; }
        }
    }

    public class CreditLine
    {
        public string UserId { get; set; }
        public string BankId { get; set; }
        public string Type { get; set; }
        public float Value { get; set; }
        public double Tax { get; set; }
        public int Installments { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
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
        public class savingFund
        {

        }
        public class privatePension
        {

        }
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

    public class Bill
    {
        public string Identity { get; set; }
        public float Value { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime PaidDate { get; set; }
    }
}