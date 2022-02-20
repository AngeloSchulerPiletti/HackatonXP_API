using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Data.VO.Febraban
{
    public class FebrabanSelectOptionsQuestion
    {
        public FebrabanSelectOptionsQuestion(int id, bool selected)
        {
            Id = id;
            Selected = selected;
        }

        public int Id { get; set; }
        public bool Selected { get; set; }
    }
}
