using log4net;
using Presentation.Web.Filters;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters, ILog logger)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LoggingFilterAttribute(logger));
            filters.Add(new LogErrorsAttribute(logger));
        }
    }
}
