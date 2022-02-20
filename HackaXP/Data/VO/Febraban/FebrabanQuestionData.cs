using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Data.VO.Febraban
{
    public class FebrabanQuestionData
    {
        public FebrabanQuestionData(string questionCode, object[] options, int value)
        {
            QuestionCode = questionCode;
            Options = options;
            Value = value;
        }

        public string QuestionCode { get; set; }
        public object[] Options { get; set; }
        public int Value { get; set; }
    }
}
