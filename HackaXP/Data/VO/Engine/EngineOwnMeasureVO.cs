using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Data.VO.Engine
{
    public class EngineOwnMeasureVO
    {
        public EngineOwnMeasureVO() {
            Scores = new List<Question>();
        }

        public List<Question> Scores { get; set; }

        //public Dictionary<int, float> PercentualScorePerQuestion 
        //{ 
        //    get; 
        //    set
        //    {
        //        TranslatedAnswer.Add()
        //    }
        //}
        //public Dictionary<int, int> TranslatedAnswer
        //{
        //    get
        //    {

        //    }
        //    set { }
        //}

        public Dictionary<string, bool> FinancialProducts { get; set; }

        public class Question
        {
            public Question(long questionId, float percentualValue)
            {
                QuestionId = questionId;
                PercentualValue = percentualValue;
            }

            public long QuestionId { get; set; }
            public float PercentualValue { get; set; }
            public double AbsoluteValue
            {
                get
                {
                    if (PercentualValue >= 1) return 5;

                    float decimalInt = PercentualValue * 5;
                    return Math.Round(decimalInt);
                }
            }
        }
    }
}
