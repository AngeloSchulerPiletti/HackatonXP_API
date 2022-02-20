using HackaXP.Data.DTO.OpenFinance;
using HackaXP.Data.VO.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HackaXP.Data.VO.Engine.EngineOwnMeasureVO;

namespace HackaXP.Business.Implementation
{
    public class EngineFinancialHealthyMeasure : IEngineFinancialHealthyMeasure
    {

        public EngineOwnMeasureVO Calculate(CostumerOpenFinanceData costumerData)
        {
            EngineOwnMeasureVO engineOwnMeasureVO = new();

            List<float> q1DecimalResult = Question1();
            engineOwnMeasureVO.Scores.Add(new Question(1, q1DecimalResult[0], (int)q1DecimalResult[1]));

            return engineOwnMeasureVO;

            List<float> Question1()
            {
                DateTime OneYearAgo = DateTime.Now.AddDays(-365).Date;

                float totalExpensesCreditCard12Months = 0.00f;
                float totalExpensesBills12Months = 0.00f;
                float totalExpensesPix12Months = 0.00f;
                float totalExpensesTransactions12Months = 0.00f;
                float totalExpensesCreditLines12Months = 0.00f;

                float totalIncomePix12Months = 0.00f;
                float totalIncomeTransactions12Months = 0.00f;
                float totalIncomeCreditCard12Months = 0.00f;

                float salary12Months = costumerData.Salary * 13;
                foreach (Bank bank in costumerData.Banks)
                {
                    foreach (Transactions transaction in bank.CreditCard.Transactions)
                    {
                        if (transaction.Date.Date > OneYearAgo)
                        {
                            if (!transaction.isEntry) totalExpensesCreditCard12Months += transaction.Value;
                            else totalIncomeCreditCard12Months += transaction.Value;
                        }
                    }

                    foreach (Bill bill in bank.Bills)
                    {
                        if (bill.PaidDate.Date > OneYearAgo) totalExpensesBills12Months += bill.Value;
                    }

                    foreach (PixTransaction pixTransaction in bank.PixHistory)
                    {
                        if (!(pixTransaction.To.Cpf == costumerData.Cpf && pixTransaction.From.Cpf == costumerData.Cpf) && (pixTransaction.Date.Date > OneYearAgo))
                        {
                            if (pixTransaction.From.Cpf == costumerData.Cpf) totalExpensesPix12Months += pixTransaction.Value;
                            else totalIncomePix12Months += pixTransaction.Value;

                        }
                    }

                    foreach (Transactions transaction in bank.Checking.Transactions)
                    {
                        if (transaction.Date.Date > OneYearAgo)
                        {
                            if (!transaction.isEntry) totalExpensesTransactions12Months += transaction.Value;
                            else totalIncomeTransactions12Months += transaction.Value;
                        }
                    }

                    foreach (CreditLine creditLine in bank.ConsumedCreditLines)
                    {
                        if ((creditLine.EndDate > OneYearAgo) && (creditLine.StartDate < DateTime.Now))
                        {
                            DateTime countDateStart =
                                (creditLine.StartDate.Date >= OneYearAgo) ?
                                creditLine.StartDate : OneYearAgo;

                            //DateTime countDateEnd =
                            //    DateTime.Compare(creditLine.EndDate.Date, DateTime.Now.Date) <= 0 ?
                            //    DateTime.Now.Date : creditLine.EndDate;

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
                                // Ao invés de paidInstallments, eleva-se à quantidade de meses correspondente
                                int paidInstallments = (int)Math.Round(paidDateRange.TotalMilliseconds / creditDateRangeChunk);

                                double totalTax = Math.Pow((1 + creditLine.Tax), paidDateRangeInMonths);
                                totalExpense = ((creditLine.Value / creditLine.Installments) * paidInstallments) * totalTax;
                            }
                            totalExpensesCreditLines12Months += (float)Math.Round(totalExpense, 2);
                        }
                    }
                }
                float totalExpensesIn12Months =
                    totalExpensesBills12Months +
                    totalExpensesCreditCard12Months +
                    totalExpensesCreditLines12Months +
                    totalExpensesPix12Months +
                    totalExpensesTransactions12Months;

                float totalIncomesIn12Months =
                    totalIncomeCreditCard12Months +
                    totalIncomePix12Months +
                    totalIncomeTransactions12Months;

                float percentualResult = (totalExpensesIn12Months / totalIncomesIn12Months);

                float translatedResult;
                if (percentualResult > 1) translatedResult = 1;
                else translatedResult = (float)Math.Round(percentualResult * 5);

                return new List<float>() { percentualResult, translatedResult };
            }
        }


    }
}
