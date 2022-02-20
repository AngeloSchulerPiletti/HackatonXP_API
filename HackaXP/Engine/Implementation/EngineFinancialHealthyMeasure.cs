using HackaXP.Data.DTO.Engine;
using HackaXP.Data.DTO.OpenFinance;
using HackaXP.Data.VO.Engine;
using HackaXP.Data.VO.Febraban;
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
        public float TotalInvestedFunds = 0.00f;
        public float TotalInvestedStocks = 0.00f;
        public float TotalInvested = 0.00f;
        public float AverageSuitability = 0.00f;

        public int HowManyBankAccounts = 0;

        public bool UsesInstallments = false;
        public bool UsesPix = false;
        public bool HaveStocks = false;
        public bool HaveAnyFixedAsset = false;
        public bool HaveFunds = false;

        public float Salary12Months;

        public EngineOwnMeasureVO Calculate(CostumerOpenFinanceData costumerData)
        {
            EngineOwnMeasureVO engineOwnMeasureVO = new();

            QuestionResultVO q1Result = Question1();
            engineOwnMeasureVO.Scores.Add(new Question("s4", q1Result.AbsolutePercentualResult, q1Result.TranslatedResult, 1));

            QuestionResultVO q2Result = Question2();
            engineOwnMeasureVO.Scores.Add(new Question("s1", q2Result.AbsolutePercentualResult, q2Result.TranslatedResult, 1));

            QuestionResultVO q3Result = Question3();
            engineOwnMeasureVO.Scores.Add(new Question("s2", q3Result.AbsolutePercentualResult, q3Result.TranslatedResult, 1));

            QuestionResultVO q4Result = Question4();
            engineOwnMeasureVO.Scores.Add(new Question("s3", q4Result.AbsolutePercentualResult, q4Result.TranslatedResult, 1));


            QuestionResultVO q5Result = Question5();
            engineOwnMeasureVO.Scores.Add(new Question("c1", q5Result.AbsolutePercentualResult, q5Result.TranslatedResult, 2));

            QuestionResultVO q6Result = Question6();
            engineOwnMeasureVO.Scores.Add(new Question("c2", q6Result.AbsolutePercentualResult, q6Result.TranslatedResult, 2));

            QuestionResultVO q7Result = Question7();
            engineOwnMeasureVO.Scores.Add(new Question("c3", q7Result.AbsolutePercentualResult, q7Result.TranslatedResult, 2));


            // Mocked data
            engineOwnMeasureVO.Scores.Add(new Question("at1", q7Result.AbsolutePercentualResult, q7Result.TranslatedResult, 3));

            engineOwnMeasureVO.Scores.Add(new Question("at2", q7Result.AbsolutePercentualResult, q7Result.TranslatedResult, 3));

            engineOwnMeasureVO.Scores.Add(new Question("at3", q7Result.AbsolutePercentualResult, q7Result.TranslatedResult, 3));


            engineOwnMeasureVO.Scores.Add(new Question("l1", q7Result.AbsolutePercentualResult, q7Result.TranslatedResult, 4));

            engineOwnMeasureVO.Scores.Add(new Question("l2", q7Result.AbsolutePercentualResult, q7Result.TranslatedResult, 4));


            engineOwnMeasureVO.Scores.Add(new Question("ap1", q7Result.AbsolutePercentualResult, q7Result.TranslatedResult, 5));

            engineOwnMeasureVO.Scores.Add(new Question("ap2", q7Result.AbsolutePercentualResult, q7Result.TranslatedResult, 5));

            engineOwnMeasureVO.Scores.Add(new Question("ap3", q7Result.AbsolutePercentualResult, q7Result.TranslatedResult, 5));

            // Essa é de selecionar...
            //engineOwnMeasureVO.SelectableQuestions.Add(new SelectableQuestion(1, "bf3", true));
            engineOwnMeasureVO.Scores.Add(new Question("bf3", q7Result.AbsolutePercentualResult, q7Result.TranslatedResult, 6)); // Isso nao serve para nada

            engineOwnMeasureVO.Scores.Add(new Question("bf2", q7Result.AbsolutePercentualResult, q7Result.TranslatedResult, 6));

            engineOwnMeasureVO.Scores.Add(new Question("bf4", q7Result.AbsolutePercentualResult, q7Result.TranslatedResult, 6));




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

                int translatedResult = (int)Math.Round(percentualResult * 5);

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
                translatedResult = (int)Math.Round(resultQuocient * 5);

                return new QuestionResultVO(translatedResult, resultQuocient);
            }

            QuestionResultVO Question3()
            {
                foreach (Bank bank in costumerData.Banks)
                {
                    TotalInvested += BaseMeasures.CalculateTotalInvestedInBank12Months(bank);
                }
                int translatedResult = 5;

                float quocientInvestimentsSalary = (OperationsCreditLine12Months.TotalFutureExpense) / (TotalInvested + Salary12Months + TotalCheckingBalance);

                translatedResult = (int)Math.Round(quocientInvestimentsSalary * 5);

                return new QuestionResultVO(translatedResult, quocientInvestimentsSalary);
            }

            QuestionResultVO Question4()
            {
                int translatedResult = 5;
                float quocientInvestimentsSalary = 1;

                if (TotalInvested > 0)
                {
                    quocientInvestimentsSalary = (OperationsCreditLine12Months.TotalFutureExpense) / (Salary12Months + TotalCheckingBalance);
                    translatedResult = (int)Math.Round(quocientInvestimentsSalary * 5);
                }


                translatedResult = (int)Math.Round(quocientInvestimentsSalary * 5);

                return new QuestionResultVO(translatedResult, quocientInvestimentsSalary);
            }

            QuestionResultVO Question5()
            {
                for (int i = 0; i < costumerData.Banks.Length; i++)
                {
                    AverageSuitability += costumerData.Banks[i].Suitablity;
                }
                AverageSuitability /= costumerData.Banks.Length;

                int translatedResult = 5;
                if (TotalInvested < 1000000) translatedResult = TotalInvested <= 1000 ? 1 : (int)Math.Round(AverageSuitability / 20);

                return new QuestionResultVO(translatedResult, translatedResult / 5);
            }

            QuestionResultVO Question6()
            {
                foreach (Bank bank in costumerData.Banks)
                {
                    TotalInvestedStocks += BaseMeasures.CalculateTotalInvestedStocksInBank12Months(bank);
                    if (TotalInvestedStocks > 1000) HaveStocks = true;
                    if (TotalInvestedFixedAssets > 1000) HaveAnyFixedAsset = true;
                    TotalInvestedFunds = BaseMeasures.CalculateTotalInvestedFundsInBank12Months(bank);
                    if (TotalInvestedFunds > 1000) HaveFunds = true;
                }

                int translatedResult = 1;
                float quocientResult = 0;
                if (AverageSuitability > 20)
                {
                    int quo1 = HaveStocks ? 1 : 0;
                    int quo2 = HaveFunds ? 1 : 0;
                    int quo3 = HaveAnyFixedAsset ? 1 : 0;
                    quocientResult = ((quo1 * 3 + quo2 * 3 + quo3 * 2) / 8);

                    translatedResult = (int)Math.Round(quocientResult * 5);
                }

                return new QuestionResultVO(translatedResult, translatedResult / 5);
            }

            QuestionResultVO Question7()
            {
                foreach (Bank bank in costumerData.Banks)
                {
                    HowManyBankAccounts++;
                    if (bank.PixHistory.Length > 0) UsesPix = true;
                }

                int translatedResult = 1;
                float quocientResult = 0.11f;
                if (AverageSuitability > 20)
                {
                    int quo1 = HaveStocks ? 1 : 0;
                    int quo2 = HaveFunds ? 1 : 0;
                    int quo3 = HaveAnyFixedAsset ? 1 : 0;
                    quocientResult = ((quo1 * 3 + quo2 * 3 + quo3 * 2) / 10);
                    quocientResult += AverageSuitability / 500;
                    quocientResult += HowManyBankAccounts / 20;
                }
                else if (UsesPix) quocientResult += 0.1f;

                translatedResult = (int)Math.Round(quocientResult * 5);

                return new QuestionResultVO(translatedResult, translatedResult / 5);
            }
        }


        public FebrabanFormVO TranslateToFebrabanJson(EngineOwnMeasureVO engineOwnMeasureVO)
        {
            List<FebrabanQuestionData> questionDataList = new List<FebrabanQuestionData>();
            List<FebrabanQuestionSection> questionsSections = new();
            questionsSections.Add(new FebrabanQuestionSection(1, questionDataList));

            foreach (Question question in engineOwnMeasureVO.Scores)
            {
                FebrabanQuestionData translatedQuestion = new(question.QuestionCode, question.TranslatedScore);
                questionDataList.Add(translatedQuestion);
            }
            for (int i = 0; i < engineOwnMeasureVO.Scores.Count; i++)
            {
                if(questionsSections.Last().Id < engineOwnMeasureVO.Scores[i].SectionId)
                {
                    questionsSections.Add(new FebrabanQuestionSection(engineOwnMeasureVO.Scores[i].SectionId, /*Preciso ver*/));
                }

            }

            FebrabanFormVO febrabanFormVO = new(questionsSections);
        }


    }
}
