using backend.Models.Context;
using HackaXP.Data.DTO;
using HackaXP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Repository.Implementation
{
    public class CostumerRepository : ICostumerRepository
    {
        private readonly MySQLContext _context;

        public CostumerRepository(MySQLContext context)
        {
            _context = context;
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
