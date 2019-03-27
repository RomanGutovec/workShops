using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingSample_BLL.Exceptions
{
    public class CustomerServiceException : Exception
    {
        public enum ErrorType
        {
            WrongCustomerId
        }

        public ErrorType Type { get; set; }

        public CustomerServiceException(string message, ErrorType errorType) : base(message)
        {
            Type = errorType;
        }
    }
}
