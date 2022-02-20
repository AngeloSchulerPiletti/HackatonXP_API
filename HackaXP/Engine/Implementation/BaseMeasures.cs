using HackaXP.Data.DTO.Engine;
using HackaXP.Data.DTO.OpenFinance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HackaXP.Data.DTO.OpenFinance.Investiments;

namespace HackaXP.Engine.Implementation
{
    public class BaseMeasures : IBaseMeasures
    {
        public static DateTime OneYearAgo = DateTime.Now.AddDays(-365).Date;

        public static Operations CalculateCreditCardTransactionInBank12Months(Bank bank)
        {
            Operations operations = new();
            foreach (Transactions transaction in bank.CreditCard.Transactions)
            {
                if (transaction.Date.Date > OneYearAgo)
                {
                    if (!transaction.isEntry) operations.Expenses += transaction.Value;
                    else operations.Incomes += transaction.Value;
                }
            }
            return operations;

        }

        public static Operations CalculateBillsPaymentInBank12Months(Bank bank)
        {
            Operations operations = new();
            foreach (Bill bill in bank.Bills)
            {
                if (bill.PaidDate.Date > OneYearAgo) operations.Expenses += bill.Value;
            }
            return operations;

        }

        public static Operations CalculatePixTransactionInBank12Months(Bank bank, string costumerCpf)
        {
            Operations operations = new();
            foreach (PixTransaction pixTransaction in bank.PixHistory)
            {
                if (!(pixTransaction.To.Cpf == costumerCpf && pixTransaction.From.Cpf == costumerCpf) && (pixTransaction.Date.Date > OneYearAgo))
                {
                    if (pixTransaction.From.Cpf == costumerCpf) operations.Expenses += pixTransaction.Value;
                    else operations.Incomes += pixTransaction.Value;

                }
            }
            return operations;
        }

        public static Operations CalculateCheckingTransactionsInBank12Months(Bank bank)
        {
            Operations operations = new();
            foreach (Transactions transaction in bank.Checking.Transactions)
            {
                if (transaction.Date.Date > OneYearAgo)
                {
                    if (!transaction.isEntry) operations.Expenses += transaction.Value;
                    else operations.Incomes += transaction.Value;
                }
            }
            return operations;
        }

        public static Operations CalculateCreditLineExpenseInBank12Months(Bank bank)
        {
            Operations operations = new();
            foreach (CreditLine creditLine in bank.ConsumedCreditLines)
            {
                if ((creditLine.EndDate > OneYearAgo) && (creditLine.StartDate < DateTime.Now))
                {
                    DateTime countDateStart =
                        (creditLine.StartDate.Date >= OneYearAgo) ?
                        creditLine.StartDate : OneYearAgo;

                    TimeSpan creditDateRange = creditLine.EndDate.Subtract(countDateStart);
                    double creditDateRangeChunk = (creditDateRange.TotalMilliseconds / (creditLine.Installments - 1));

                    double totalExpense = 0;

                    TimeSpan paidDateRange = DateTime.Now.Date.Subtract(countDateStart);
                    int paidDateRangeInMonths = (int)Math.Round(paidDateRange.TotalDays / 30.4);

                    if (creditLine.EndDate.Date <= DateTime.Now.Date)
                    {
                        double totalTax = Math.Pow((1 + creditLine.Tax), paidDateRangeInMonths);
                        totalExpense = creditLine.Value * totalTax;
                    }
                    else
                    {
                        int paidInstallments = (int)Math.Round(paidDateRange.TotalMilliseconds / creditDateRangeChunk);

                        double totalTax = Math.Pow((1 + creditLine.Tax), paidDateRangeInMonths);
                        totalExpense = ((creditLine.Value / creditLine.Installments) * paidInstallments) * totalTax;
                    }
                    operations.Expenses += (float)Math.Round(totalExpense, 2);
                }
            }
            return operations;
        }

        public static Operations CalculateFutureCreditLineExpenseInBank12Months(Bank bank)
        {
            Operations operations = new();
            foreach (CreditLine creditLine in bank.ConsumedCreditLines)
            {
                if ((creditLine.EndDate > OneYearAgo) && (creditLine.StartDate < DateTime.Now))
                {
                    DateTime countDateStart =
                        (creditLine.StartDate.Date >= OneYearAgo) ?
                        creditLine.StartDate : OneYearAgo;

                    TimeSpan creditDateRange = creditLine.EndDate.Subtract(countDateStart);
                    double creditDateRangeChunk = (creditDateRange.TotalMilliseconds / (creditLine.Installments - 1));

                    double totalExpense = 0;

                    TimeSpan paidDateRange = DateTime.Now.Date.Subtract(countDateStart);
                    int paidDateRangeInMonths = (int)Math.Round(paidDateRange.TotalDays / 30.4);

                    if (creditLine.EndDate.Date <= DateTime.Now.Date)
                    {
                        double totalTax = Math.Pow((1 + creditLine.Tax), paidDateRangeInMonths);
                        totalExpense = creditLine.Value * totalTax;
                    }
                    else
                    {
                        int paidInstallments = (int)Math.Round(paidDateRange.TotalMilliseconds / creditDateRangeChunk);

                        double totalTax = Math.Pow((1 + creditLine.Tax), paidDateRangeInMonths);
                        totalExpense = ((creditLine.Value / creditLine.Installments) * paidInstallments) * totalTax;
                    }
                    operations.Expenses += (float)Math.Round(totalExpense, 2);
                }
            }
            return operations;
        }

        public static float CalculateTotalInvestedInBank12Months(Bank bank)
        {
            return CalculateTotalInvestedFundsInBank12Months(bank) +
                CalculateTotalInvestedStocksInBank12Months(bank) +
                CalculateTotalInvestedInFixedAssetsInBank12Months(bank);
        }

        public static float TotalInvestedLowRiskInBank12Months(Bank bank)
        {
            return CalculateTotalInvestedFundsInBank12Months(bank, 0, 40) +
                CalculateTotalInvestedInFixedAssetsInBank12Months(bank);
        }

        public static float TotalInvestedHighRiskInBank12Months(Bank bank)
        {
            return CalculateTotalInvestedFundsInBank12Months(bank, 40) +
                CalculateTotalInvestedInFixedAssetsInBank12Months(bank);
        }

        public static float CalculateTotalInvestedFundsInBank12Months(Bank bank, int minRisk = 0, int maxRisk = 100)
        {
            float totalInvested = 0.00f;
            foreach (investimentFund fund in bank.Investiments.InvestimentFunds)
            {
                if (fund.Risk >= minRisk && fund.Risk <= maxRisk)
                {
                    totalInvested += fund.Value * fund.Volume;
                }
            }
            return totalInvested;
        }

        public static float CalculateTotalInvestedStocksInBank12Months(Bank bank)
        {
            float totalInvested = 0.00f;
            foreach (Stock stock in bank.Investiments.Stocks)
            {
                totalInvested += stock.Value * stock.Volume;
            }
            return totalInvested;
        }

        public static float CalculateTotalInvestedInFixedAssetsInBank12Months(Bank bank)
        {
            float totalInvested = 0.00f;
            foreach (CdbAsset cdb in bank.Investiments.Cdb)
            {
                totalInvested += cdb.Value * cdb.Volume;
            }

            foreach (FixedIncomeAsset lci in bank.Investiments.Lci)
            {
                totalInvested += lci.Value * lci.Volume;
            }

            foreach (FixedIncomeAsset lca in bank.Investiments.Lca)
            {
                totalInvested += lca.Value * lca.Volume;
            }

            foreach (FixedIncomeAsset cri in bank.Investiments.Cri)
            {
                totalInvested += cri.Value * cri.Volume;
            }

            foreach (FixedIncomeAsset cra in bank.Investiments.Cra)
            {
                totalInvested += cra.Value * cra.Volume;
            }
            return totalInvested;
        }

    }
}
