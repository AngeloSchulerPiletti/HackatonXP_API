using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Data.VO.Febraban
{
    public class FebrabanQuestionSection
    {
        public FebrabanQuestionSection(int id, List<FebrabanQuestionData> questions)
        {
            Id = id;
            Questions = questions;
        }

        public int Id { get; set; }
        public List<FebrabanQuestionData> Questions { get; set; }
    }
}
