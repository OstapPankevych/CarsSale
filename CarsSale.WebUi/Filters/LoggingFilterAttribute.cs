using System.Web.Mvc;
using CarsSale.WebUi.Support;

namespace CarsSale.WebUi.Filters
{
    public class LoggingFilterAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LogProvider.Logger.Info(filterContext.ToString());
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            LogProvider.Logger.Info(filterContext.ToString());
            base.OnActionExecuted(filterContext);
        }
    }
}