using HackaXP.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Models.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext()
        {

        }
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
        {

        }
        public DbSet<Costumer> Costumers { get; set; }
        public DbSet<FinancialHealthyHistory> FinancialHealthyHistorys { get; set; }
    }
}
