using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingSample_BLL.Exceptions
{
    public class OrderServiceException : Exception
    {
        public enum ErrorType
        {
            WrongOrderId
        }

        public ErrorType Type { get; set; }

        public OrderServiceException(string message, ErrorType errorType) : base(message)
        {
            Type = errorType;
        }
    }
}
