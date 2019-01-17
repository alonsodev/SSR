using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Web.Filters
{
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        log4net.ILog logger = null;
        public LoggingFilterAttribute(log4net.ILog logger)
        {
            this.logger = logger;
        }


        private const string StopwatchKey = "DebugLoggingStopWatch";


    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (logger.IsDebugEnabled)
        {
            var loggingWatch = Stopwatch.StartNew();
            filterContext.HttpContext.Items.Add(StopwatchKey, loggingWatch);

            var message = new StringBuilder();
            message.Append(string.Format("Executing controller {0}, action {1}", 
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, 
                filterContext.ActionDescriptor.ActionName));

            logger.Debug(message);
        }
    }

    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
        if (logger.IsDebugEnabled)
        {
            if (filterContext.HttpContext.Items[StopwatchKey] != null)
            {
                var loggingWatch = (Stopwatch)filterContext.HttpContext.Items[StopwatchKey];
                loggingWatch.Stop();

                long timeSpent = loggingWatch.ElapsedMilliseconds;

                var message = new StringBuilder();
                message.Append(string.Format("Finished executing controller {0}, action {1} - time spent {2}",
                    filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                    filterContext.ActionDescriptor.ActionName,
                    timeSpent));

                logger.Debug(message);
                filterContext.HttpContext.Items.Remove(StopwatchKey);
            }              
        }
    }
    }
}