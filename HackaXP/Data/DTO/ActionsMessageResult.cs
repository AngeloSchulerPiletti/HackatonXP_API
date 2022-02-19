using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaXP.Data.DTO
{
    public class ActionsMessageResult
    {
        public ActionsMessageResult(string message, bool isError = true)
        {
            Message = message;
            IsError = isError;
        }

        public string Message { get; set; }
        public bool IsError { get; set; }
    }
}
