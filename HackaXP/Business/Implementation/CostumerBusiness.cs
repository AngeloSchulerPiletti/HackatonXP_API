using HackaXP.Business.Implementation;
using HackaXP.Models;
using HackaXP.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Business
{
    public class CostumerBusiness : ICostumerBusiness
    {
        private ICostumerRepository _costumerRepository;

        public CostumerBusiness(ICostumerRepository costumerRepository)
        {
            _costumerRepository = costumerRepository;
        }

        public bool CostumerAllowTest(long costumerId)
        {
            Costumer costumer = _costumerRepository.GetCostumerData(costumerId);
            return costumer.AllowTest;
        }
        public bool CostumerAllowTest(string costumerName)
        {
            Costumer costumer = _costumerRepository.GetCostumerData(costumerName);
            return costumer.AllowTest;
        }
    }
}
