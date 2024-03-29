﻿
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
        public List<PixTransaction> PixHistory { get; set; }
        public List<CreditLine> ConsumedCreditLines { get; set; }
        public Investments Investments { get; set; }
        public List<Bill> Bills { get; set; }
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
        public CreditCard()
        {
            Transactions = new List<Transactions>();
        }

        public float Limit { get; set; }
        public List<Transactions> Transactions { get; set; }
        public bool InstallmentsUsage { get; set; }
    }

    public class Checking
    {
        public Checking()
        {
            Transactions = new List<Transactions>();
        }

        public float Balance { get; set; }
        public float Limit { get; set; }
        public List<Transactions> Transactions { get; set; }

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

    public class Bill
    {
        public string Identity { get; set; }
        public float Value { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime PaidDate { get; set; }
    }
}