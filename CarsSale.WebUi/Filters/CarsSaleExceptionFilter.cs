using System.Net;
using System.Web.Mvc;
using CarsSale.WebUi.Logger;

namespace CarsSale.WebUi.Filters
{
    public class CarsSaleExceptionFilter : FilterAttribute, IExceptionFilter
    {
        private readonly ILogger _logger;

        public CarsSaleExceptionFilter() { }

        public CarsSaleExceptionFilter(ILogger logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled) return;
            _logger.Log(filterContext.Exception);
            filterContext.ExceptionHandled = true;
            filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }
    }
}