using HackaXP.Data.DTO.Engine;
using HackaXP.Data.DTO.OpenFinance;
using HackaXP.Data.VO.Engine;
using HackaXP.Engine.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HackaXP.Data.VO.Engine.EngineOwnMeasureVO;

namespace HackaXP.Business.Implementation
{
    public class EngineFinancialHealthyMeasure : IEngineFinancialHealthyMeasure
    {
        public Operations OperationsBills12Months = new();
        public Operations OperationsPix12Months = new();
        public Operations OperationsCreditCard12Months = new();
        public Operations OperationsCreditLine12Months = new();
        public Operations OperationsChecking12Months = new();

        public float TotalInvestedFixedAssets = 0.00f;
        public float TotalSavingBalance = 0.00f;
        public float TotalCheckingBalance = 0.00f;
        public float TotalInvestedLowRiskFunds = 0.00f;
        public float TotalInvested = 0.00f;

        public bool UsesInstallments = false;

        public float Salary12Months;

        public EngineOwnMeasureVO Calculate(CostumerOpenFinanceData costumerData)
        {
            EngineOwnMeasureVO engineOwnMeasureVO = new();

            QuestionResultVO q1Result = Question1();
            engineOwnMeasureVO.Scores.Add(new Question(1, q1Result.AbsolutePercentualResult, q1Result.TranslatedResult));

            QuestionResultVO q2Result = Question2();
            engineOwnMeasureVO.Scores.Add(new Question(2, q2Result.AbsolutePercentualResult, q2Result.TranslatedResult));



            return engineOwnMeasureVO;

            QuestionResultVO Question1()
            {
                DateTime OneYearAgo = DateTime.Now.AddDays(-365).Date;

                Salary12Months = costumerData.Salary * 13;

                float totalExpenses = 0.00f;
                float totalIncomes = 0.00f;
                foreach (Bank bank in costumerData.Banks)
                {
                    Operations operationsCreditCard = BaseMeasures.CalculateCreditCardTransactionInBank12Months(bank);
                    OperationsCreditCard12Months.Expenses += operationsCreditCard.Expenses;
                    OperationsCreditCard12Months.Incomes += operationsCreditCard.Incomes;

                    Operations operationsBills = BaseMeasures.CalculateBillsPaymentInBank12Months(bank);
                    OperationsBills12Months.Expenses += operationsBills.Expenses;
                    OperationsBills12Months.Incomes += operationsBills.Incomes;

                    Operations operationsChecking = BaseMeasures.CalculateCheckingTransactionsInBank12Months(bank);
                    OperationsChecking12Months.Expenses += operationsBills.Expenses;
                    OperationsChecking12Months.Incomes += operationsBills.Incomes;

                    Operations operationsPix = BaseMeasures.CalculatePixTransactionInBank12Months(bank, costumerData.Cpf);
                    OperationsPix12Months.Expenses += operationsBills.Expenses;
                    OperationsPix12Months.Incomes += operationsBills.Incomes;

                    Operations operationsCreditLine = BaseMeasures.CalculateCreditLineExpenseInBank12Months(bank);
                    OperationsCreditLine12Months.Expenses += operationsBills.Expenses;
                    OperationsCreditLine12Months.Incomes += operationsBills.Incomes;

                    totalExpenses +=
                        (operationsBills.Expenses +
                        operationsChecking.Expenses +
                        operationsCreditCard.Expenses +
                        operationsPix.Expenses +
                        operationsCreditLine.Expenses);

                    totalIncomes +=
                        (operationsBills.Incomes +
                        operationsChecking.Incomes +
                        operationsCreditCard.Incomes +
                        operationsPix.Incomes +
                        operationsCreditLine.Incomes);
                }
                totalIncomes += Salary12Months;

                float percentualResult = (totalExpenses / totalIncomes);

                int translatedResult;
                if (percentualResult > 1) translatedResult = 1;
                else translatedResult = (int)Math.Round(percentualResult * 5);

                return new QuestionResultVO(translatedResult, percentualResult);
            }

            QuestionResultVO Question2()
            {
                foreach (Bank bank in costumerData.Banks)
                {
                    TotalInvestedFixedAssets += BaseMeasures.CalculateTotalInvestedInFixedAssetsInBank12Months(bank);
                    TotalInvestedLowRiskFunds += BaseMeasures.CalculateTotalInvestedFundsInBank12Months(bank, 0, 40);

                    TotalSavingBalance += bank.Saving.Balance;
                    TotalCheckingBalance += bank.Checking.Balance;

                    if (bank.CreditCard.InstallmentsUsage) UsesInstallments = true;
                }

                int translatedResult = 5;

                float quocientMonthlyExpensesIncomes = (OperationsBills12Months.Expenses + OperationsPix12Months.Expenses) / (Salary12Months + OperationsPix12Months.Incomes);
                float quocientInvestimentsBills = OperationsBills12Months.Expenses / (TotalInvestedFixedAssets + TotalSavingBalance + TotalInvestedLowRiskFunds);
                float quocientCheckingBalanceCreditLines = OperationsCreditLine12Months.Expenses / TotalCheckingBalance;

                float resultQuocient = ((quocientCheckingBalanceCreditLines + quocientInvestimentsBills + (quocientMonthlyExpensesIncomes * 2)) / 4);

                if (UsesInstallments)
                {
                    resultQuocient += 0.17f;
                }
                if (resultQuocient < 1)
                {
                    translatedResult = (int)Math.Round(resultQuocient * 5);
                }

                return new QuestionResultVO(translatedResult, resultQuocient);
            }

            QuestionResultVO Question3()
            {
                foreach (Bank bank in costumerData.Banks)
                {
                    TotalInvested += BaseMeasures.CalculateTotalInvestedInBank12Months(bank);
                }
                float quocientInvestimentsSalary = (OperationsCreditLine12Months.Expenses + ) /(TotalInvested + Salary12Months + TotalCheckingBalance);
            }
        }


    }
}
