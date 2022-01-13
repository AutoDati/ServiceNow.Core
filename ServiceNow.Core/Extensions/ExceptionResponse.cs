using System;
using System.Collections.Generic;
using System.Text;

namespace SNow.Core.Extensions
{
    public class ExceptionResponse
    {
        public ErrorResponse Error { get; set; }
        public string Status { get; set; }
    }

    public class ErrorResponse
    {
        public string Detail { get; set; }
        public string Message { get; set; }
    }
}
