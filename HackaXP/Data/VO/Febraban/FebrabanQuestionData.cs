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
            List<Option> options = new();
            for (int i = 0; i < 5; i++)
            {
                options.Add(new Option(i, i + 1));
            }
            Options = options;
            Value = value;
        }

        public string QuestionCode { get; set; }
        public int Value { get; set; }
        public List<Option> Options { get; set; }

        public class Option
        {
            public Option(int cod, int id)
            {
                Cod = cod;
                Id = id;
            }

            public int Cod { get; set; }
            public int Id { get; set; }
        }
    }
}
