using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopBridge.Web.Filter
{
    public class CustomExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && filterContext.Exception is Exception)
            {
                var message = filterContext.Exception.Message;
                if (filterContext.Exception.Message.Length > 100 || String.IsNullOrEmpty(filterContext.Exception.Message))
                    message = "Unauthorized access to the resources";

                var url =  "/Home/Error?message=" + message;  
                filterContext.Result = new RedirectResult(url);
                filterContext.ExceptionHandled = true;
            }
        }
    }
}