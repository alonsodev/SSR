using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Web.Filters
{
    public class LogErrorsAttribute : FilterAttribute, IExceptionFilter
    {
        #region IExceptionFilter Members
        log4net.ILog logger = null;

        public LogErrorsAttribute(log4net.ILog logger)
        {
            this.logger = logger;
        }

        void IExceptionFilter.OnException(ExceptionContext filterContext)
        {
            if (filterContext != null && filterContext.Exception != null)
            {
                string controller = filterContext.RouteData.Values["controller"].ToString();
                string action = filterContext.RouteData.Values["action"].ToString();
                string loggerName = string.Format("{0}Controller.{1}", controller, action);

                logger.Error(loggerName, filterContext.Exception);

            }

        }

        #endregion
    }
}