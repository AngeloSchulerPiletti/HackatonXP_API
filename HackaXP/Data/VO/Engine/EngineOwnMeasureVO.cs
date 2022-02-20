using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Data.VO.Engine
{
    public class EngineOwnMeasureVO
    {
        public EngineOwnMeasureVO()
        {
            Scores = new List<Question>();
        }

        public List<Question> Scores { get; set; }

        public Dictionary<string, bool> FinancialProducts { get; set; }

        public class Question
        {
            public Question(long questionId, float percentualValue, int translatedScore)
            {
                QuestionId = questionId;
                PercentualValue = percentualValue;
                TranslatedScore = translatedScore;
            }

            public long QuestionId { get; set; }
            public float PercentualValue { get; set; }
            public int TranslatedScore { get; set; }
        }
    }
}
