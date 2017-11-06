using System.Net;
using System.Web.Mvc;
using CarsSale.WebUi.Support;

namespace CarsSale.WebUi.Filters
{
    public class ExceptionLoggingFilterAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled) return;
            LogProvider.Logger.Warn(filterContext.Exception);
            filterContext.ExceptionHandled = true;
            filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }
    }
}