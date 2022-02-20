using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Data.DTO.Engine
{
    public class QuestionResultVO
    {
        public QuestionResultVO(int translatedResult, float absolutePercentualResult)
        {
            TranslatedResult = translatedResult < 1 ? 1 : (translatedResult > 5 ? 5 : translatedResult);
            AbsolutePercentualResult = absolutePercentualResult;
        }

        public int TranslatedResult { get; }
        public float AbsolutePercentualResult { get; }
    }
}
