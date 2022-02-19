using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Models
{
    public class Costumer
    {
        public Costumer(string name, long lastFinancialHealthyHistoryId, bool? allowTest, bool? allowOpenFinance)
        {
            Name = name;
            LastFinancialHealthyHistoryId = lastFinancialHealthyHistoryId;
            AllowTest = allowTest;
            AllowOpenFinance = allowOpenFinance;
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("last_financial_healthy_history_id")]
        public long LastFinancialHealthyHistoryId { get; set; }

        [Column("allow_test")]
        public bool? AllowTest { get; set; }

        [Column("allow_open_finance")]
        public bool? AllowOpenFinance { get; set; }

    }
}
