using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Data.VO.Febraban
{
    public class FebrabanQuestionData
    {
        public FebrabanQuestionData(string questionCode, int value)
        {
            QuestionCode = questionCode;
            Options = new List<object>();
            Value = value;
        }

        public string QuestionCode { get; set; }
        public int Value { get; set; }
        public List<object> Options { get; set; }
    }
}
