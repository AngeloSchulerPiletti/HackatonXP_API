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
            SelectableQuestions = new List<SelectableQuestion>();
        }

        public List<Question> Scores { get; set; }
        public List<SelectableQuestion> SelectableQuestions { get; set; }
        public Dictionary<string, bool> FinancialProducts { get; set; }


        public class SelectableQuestion
        {
            public SelectableQuestion(int selectableId, string questionCode, bool selected)
            {
                SelectableId = selectableId;
                QuestionCode = questionCode;
                Selected = selected;
            }

            public int SelectableId { get; set; }
            public string QuestionCode { get; set; }
            public bool Selected { get; set; }
        }

        public class Question
        {
            public Question(string questionCode, float percentualValue, int translatedScore, int sectionId)
            {
                QuestionCode = questionCode;
                SectionId = sectionId;
                PercentualValue = percentualValue;
                TranslatedScore = translatedScore;
            }

            public string QuestionCode { get; set; }
            public int SectionId { get; set; }
            public float PercentualValue { get; set; }
            public int TranslatedScore { get; set; }
        }
    }
}
