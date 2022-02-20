using HackaXP.Data.VO.Febraban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Data.VO.Engine
{
    public class FebrabanFormVO
    {
        public FebrabanFormVO(List<FebrabanQuestionSection> data)
        {
            Data = data;
        }

        public List<FebrabanQuestionSection> Data { get; set; }
    }
}
