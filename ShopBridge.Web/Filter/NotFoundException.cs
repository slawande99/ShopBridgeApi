using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBridge.Web.Filter
{
    public class NotFoundException : ApplicationException
    {        
        public NotFoundException(string message) : base(message)
        {

        }
    }

    public class InternalServerErrorException : ApplicationException
    {
        public InternalServerErrorException(string message) : base(message)
        {

        }
    }

    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string message) : base(message)
        {

        }
    }
}