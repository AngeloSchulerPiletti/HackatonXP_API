using backend.Models.Context;
using HackaXP.Data.DTO;
using HackaXP.Data.DTO.Febraban;
using HackaXP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HackaXP.Data.DTO.Febraban.FebrabanCompleteResultData;
using static HackaXP.Data.DTO.Febraban.FebrabanCompleteResultData.DataIn;

namespace HackaXP.Repository.Implementation
{
    public class CostumerRepository : ICostumerRepository
    {
        private readonly MySQLContext _context;

        public CostumerRepository(MySQLContext context)
        {
            _context = context;
        }

        public ActionsMessageResult AddCostumer(NewCostumer newCostumer)
        {
            try
            {
                Costumer costumer = new(newCostumer.Name, newCostumer.AllowTest, newCostumer.AllowOpenFinance);
                _context.Costumers.Add(costumer);
                _context.SaveChanges();
                return new ActionsMessageResult("OpenFinance liberado com sucesso!", false);
            }
            catch
            {
                return new ActionsMessageResult("Algo deu errado ao aprovar o OpenFinance, tente mais tarde");
            }
        }

        public bool CheckIfCostumerExists(long costumerId)
        {
            Costumer costumer = _context.Costumers.FirstOrDefault(c => c.Id == costumerId);
            return costumer != null;
        }

        public bool CheckIfCostumerExists(string costumerName)
        {
            Costumer costumer = _context.Costumers.FirstOrDefault(c => c.Name == costumerName);
            return costumer != null;
        }

        public Costumer GetCostumerData(long costumerId)
        {
            Costumer costumer = _context.Costumers.FirstOrDefault(c => c.Id == costumerId);
            return costumer;
        }

        public Costumer GetCostumerData(string costumerName)
        {
            Costumer costumer = _context.Costumers.FirstOrDefault(c => c.Name == costumerName);
            return costumer;
        }

        public FinancialHealthyHistory GetLastFinancialHealthyConsult(int userId)
        {
            throw new NotImplementedException();
        }

        public FinancialHealthyHistory GetLastFinancialHealthyConsult(string userName)
        {
            throw new NotImplementedException();
        }

        public FinancialHealthyHistory SaveFinancialHealthyConsult(FebrabanCompleteResultData febrabanResponse, long costumerId)
        {
            SummaryIn febrabanSummary = febrabanResponse.Data.Summary;
            FinancialHealthyHistory financialHealthyConsult = new(
                costumerId, febrabanSummary.FinancialSecurityScore,
                febrabanSummary.FinancialKnowledgeScore,
                febrabanSummary.FinancialBehaviorScore,
                febrabanSummary.FinancialFreedomScore,
                febrabanSummary.IndexScore);

            try
            {
                _context.FinancialHealthyHistorys.Add(financialHealthyConsult);
                Costumer costumer = _context.Costumers.First(c => c.Id == costumerId);
                costumer.LastFinancialHealthyHistoryId = financialHealthyConsult.Id;
                _context.SaveChanges();

                return financialHealthyConsult;
            }
            catch
            {
                return null;
            }
        }

        public ActionsMessageResult UpdateCostumerAcceptance(CostumerAcceptance acceptanceDto)
        {
            try
            {
                Costumer costumer = _context.Costumers.First(u => u.Id == acceptanceDto.CostumerId);
                costumer.AllowTest = acceptanceDto.Acceptance;
                _context.SaveChanges();
                return new ActionsMessageResult("Alteração realizada com sucesso", false);
            }
            catch
            {
                return new ActionsMessageResult("Algo deu errado, tente mais tarde");
            }
        }
    }
}
