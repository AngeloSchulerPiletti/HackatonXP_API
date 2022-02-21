using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Models
{
    public class FinancialHealthyHistory
    {
        public FinancialHealthyHistory(long costumerId, int financialSecurityScore, int financialKnowledgeScore, int financialBehaviorScore, int financialFreedomScore, int indexScore)
        {
            CostumerId = costumerId;
            FinancialBehaviorScore = financialBehaviorScore;
            FinancialFreedomScore = financialFreedomScore;
            FinancialKnowledgeScore = financialKnowledgeScore;
            FinancialSecurityScore = financialSecurityScore;
            IndexScore = indexScore;
            ConsultDate = DateTime.Now;
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("consult_date")]
        public DateTime ConsultDate { get; set; }

        [Column("costumer_id")]
        public long CostumerId { get; set; }

        [Column("financial_behavior_score")]
        public int FinancialBehaviorScore { get; set; }

        [Column("financial_freedom_score")]
        public int FinancialFreedomScore { get; set; }

        [Column("financial_knowledge_score")]
        public int FinancialKnowledgeScore { get; set; }

        [Column("financial_security_score")]
        public int FinancialSecurityScore { get; set; }

        [Column("index_score")]
        public int IndexScore { get; set; }
    }
}
