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
        public Costumer(long id, string name, long lastFinancialHealthyHistoryId)
        {
            Id = id;
            Name = name;
            LastFinancialHealthyHistoryId = lastFinancialHealthyHistoryId;
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("last_financial_healthy_history_id")]
        public long LastFinancialHealthyHistoryId { get; set; }

    }
}
