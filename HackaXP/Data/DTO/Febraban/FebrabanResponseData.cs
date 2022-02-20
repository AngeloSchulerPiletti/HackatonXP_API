using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Data.DTO.Febraban
{
    public class FebrabanResponseData
    {
        public FebrabanResponseData(bool success, string data, string userToken)
        {
            Success = success;
            Data = data;
            UserToken = userToken;
        }

        public bool Success { get; set; }
        public string Data { get; set; }
        public string UserToken { get; set; }
    }
}
